﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace AlexaAltTran.Helper
{
    public class AlexaRequestValidationHandler : System.Net.Http.DelegatingHandler
    {

        protected async override  Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellation)
        {
            if (!request.Headers.Contains("Signature") || !request.Headers.Contains("SignatureCertChainUrl"))
                throw new HttpResponseException(new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest));


            var signatureCertChainUrl = request.Headers.GetValues("SignatureCertChainUrl").First().Replace("/../", "/");

            if (string.IsNullOrWhiteSpace(signatureCertChainUrl))
                throw new HttpResponseException(new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest));

            var certUrl = new Uri(signatureCertChainUrl);

            if (!((certUrl.Port == 443 || certUrl.IsDefaultPort)
                && certUrl.Scheme.Equals(Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase)
                && certUrl.Host.Equals("s3.amazonaws.com", StringComparison.OrdinalIgnoreCase)
                && certUrl.AbsolutePath.StartsWith("/echo.api/")
                ))
                throw new HttpResponseException(new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest));

            using (var web = new System.Net.WebClient())
            {

                var certificate = web.DownloadData(certUrl);
                var cert = new X509Certificate2(certificate);

                var effectiveDate = DateTime.MinValue;
                var expiryDate = DateTime.MinValue;

                if (!((DateTime.TryParse(cert.GetExpirationDateString(), out expiryDate)
                    && expiryDate > DateTime.UtcNow)
                    && (DateTime.TryParse(cert.GetEffectiveDateString(), out effectiveDate)
                    && effectiveDate < DateTime.UtcNow)))
                    throw new HttpResponseException(new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest));



                if (!cert.Subject.Contains("CN=echo-api.amazon.com") || !cert.Issuer.Contains("CN=Symantec Class 3 Secure Server CA"))
                {                   
                    throw new HttpResponseException(new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest));
                }

                var signatureString = request.Headers.GetValues("Signature").First();

                byte[] signature = Convert.FromBase64String(signatureString);

                using (var sha1 = new System.Security.Cryptography.SHA1Managed())
                {
                    var body = await request.Content.ReadAsStringAsync();
                    UnicodeEncoding encoding = new UnicodeEncoding();
                    var data = sha1.ComputeHash(encoding.GetBytes(body));
                    var rsa = (RSACryptoServiceProvider)cert.PublicKey.Key;

                    if (rsa == null || rsa.VerifyHash(data, CryptoConfig.MapNameToOID("SHA1"), signature))
                        throw new HttpResponseException(new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest));



                }
                
                  byte[] bodyReq = await request.Content.ReadAsByteArrayAsync();  

          


            }

            return await base.SendAsync(request, cancellation);
      
           

        }

    }
}
