using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlexaAltTran.Helper
{
    public static class StateConversion
    {
        public static string ConvertCategory(String cat)
        {

            string fourLetterCode = string.Empty;

            switch (cat.ToUpper())
            {

                case "TAX":
                    fourLetterCode = "TAX";
                    break;
                case "REBATE":
                    fourLetterCode = "RBATE";
                    break;
                case "LOANS":
                    fourLetterCode = "LOANS";
                    break;
                case "GRANTS":
                    fourLetterCode = "GNT";
                    break;
                case "EXEMPTIONS":
                    fourLetterCode = "EXEM";
                    break;
                case "REGISTRATION":
                    fourLetterCode = "REGIS";
                    break;
                case "OTHER":
                    fourLetterCode = "OTHER";
                    break;
               
                default:
                    fourLetterCode = "OTHER";
                    break;
            }

            return fourLetterCode;
        }

        public static string ConvertFuelType(String fuelType)
        {

            string fourLetterCode = string.Empty;

            switch (fuelType.ToUpper())
            {
                
                case "ALL":
                    fourLetterCode = "all";
                    break;
                case "BIODIESEL":
                    fourLetterCode = "BD";
                    break;
                case "CNG":
                    fourLetterCode = "CNG";
                    break;
                case "ELECTRIC":
                    fourLetterCode = "ELEC";
                    break;
                case "ETHANOL":
                    fourLetterCode = "E85";
                    break;
                case "HYDROGEN":
                    fourLetterCode = "HY";
                    break;
                case "LNG":
                    fourLetterCode = "LNG";
                    break;
                case "LPG":
                    fourLetterCode = "LPG";
                    break;
                default:
                    fourLetterCode = "all";
                    break;
            }

            return fourLetterCode;
            }
        public static string ConvertState(string state)
        {
            string twoLetterCode = string.Empty;

            switch (state.ToUpper())
            {
                case "ALABAMA":
                    twoLetterCode = "AL";
                    break;
                case "ALASKA":
                    twoLetterCode = "AK";
                    break;
                case "ARIZONA":
                    twoLetterCode = "AZ";
                    break;
                case "CALIFORNIA":
                    twoLetterCode = "CA";
                    break;
                case "COLORADO":
                    twoLetterCode = "CO";
                    break;
                case "CONNECTICUT":
                    twoLetterCode = "CT";
                    break;
                case "DELAWARE":
                    twoLetterCode = "DE";
                    break;
                case "FLORIDA":
                    twoLetterCode = "FL";
                    break;
                case "GEORGIA":
                    twoLetterCode = "GA";
                    break;
                case "HAWAII":
                    twoLetterCode = "HI";
                    break;
                case "IDAHO":
                    twoLetterCode = "ID";
                    break;
                case "ILLINOIS":
                    twoLetterCode = "IL";
                    break;
                case "INDIANA":
                    twoLetterCode = "IN";
                    break;
                case "IOWA":
                    twoLetterCode = "IA";
                    break;
                case "KANSAS":
                    twoLetterCode = "KS";
                    break;
                case "KENTUCKY":
                    twoLetterCode = "KY";
                    break;
                case "LOUISIANA":
                    twoLetterCode = "LA";
                    break;
                case "MAINE":
                    twoLetterCode = "ME";
                    break;
                case "MARYLAND":
                    twoLetterCode = "MD";
                    break;
                case "MASSACHUSETS":
                    twoLetterCode = "MA";
                    break;
                case "MICHIGAN":
                    twoLetterCode = "MI";
                    break;
                case "MINNESOTA":
                    twoLetterCode = "MN";
                    break;
                case "MISSISSIPPI":
                    twoLetterCode = "MS";
                    break;
                case "MISSOURI":
                    twoLetterCode = "MO";
                    break;
                case "MONTANA":
                    twoLetterCode = "MT";
                    break;
                case "NEBRASKA":
                    twoLetterCode = "NE";
                    break;
                case "NEVADA":
                    twoLetterCode = "NV";
                    break;
                case "NEW HAMPSHIRE":
                    twoLetterCode = "NH";
                    break;
                case "NEW JERSEY":
                    twoLetterCode = "NJ";
                    break;
                case "NEW MEXICO":
                    twoLetterCode = "NM";
                    break;
                case "NEW YORK":
                    twoLetterCode = "NY";
                    break;
                case "NORTH CAROLINA":
                    twoLetterCode = "NC";
                    break;
                case "NORTH DAKOTA":
                    twoLetterCode = "ND";
                    break;
                case "OHIO":
                    twoLetterCode = "OH";
                    break;
                case "OKLAHOMA":
                    twoLetterCode = "OK";
                    break;
                case "OREGON":
                    twoLetterCode = "OR";
                    break;
                case "PENNSYLVANIA":
                    twoLetterCode = "PA";
                    break;
                case "Rhode Island":
                    twoLetterCode = "PA";
                    break;
                case "SOUTH CAROLINA":
                    twoLetterCode = "SC";
                    break;
                case "SOUTH DAKOTA":
                    twoLetterCode = "SD";
                    break;
                case "TENNESSEE":
                    twoLetterCode = "TN";
                    break;
                case "TEXAS":
                    twoLetterCode = "TX";
                    break;
                case "UTAH":
                    twoLetterCode = "UT";
                    break;
                case "VERMONT":
                    twoLetterCode = "VT";
                    break;
                case "VIRGINIA":
                    twoLetterCode = "VA";
                    break;
                case "WASHINGTON":
                    twoLetterCode = "WA";
                    break;
                case "WEST VIRGINIA":
                    twoLetterCode = "WV";
                    break;
                case "WISCONSIN":
                    twoLetterCode = "WI";
                    break;
                case "WYOMING":
                    twoLetterCode = "WY";
                    break;
                default:
                    twoLetterCode = "VA";
                    break;
            }

            return twoLetterCode;
        }
    }
}