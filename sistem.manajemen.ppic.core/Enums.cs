using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.core
{
    public class Enums
    {
        public enum MenuList
        {
            Login=0,
            Home=1,
            Transaction=2,
            Gudang=3,
            Master=4,
            Report=5,
           
            TrnSpb=20,
            TrnDo=21,
            TrnPengiriman=22,

            GdgBarangJadi=30,
            GdgBarangBB = 30,

            MasterWilayah = 40,
            MasterSupplier=41,
            MasterKonsumen=42,
            MasterTransportir = 43,

            LaporanOutstanding=50,
            LaporanRekapBulanan=51,
        }
        public enum Kemasan
        {
            [Description("25 Kg/zak")]
            kg25,
            [Description ("50 Kg/zak")]
            kg50,
            [Description("Jumbo Bag / Hi Blow")]
            JumboBag,
        }
        public enum Bentuk
        {
            [Description("Tablet")]
            Tablet,
            [Description("Granule")]
            Granule,
            [Description("Powder")]
            Powder,
            [Description("Briket")]
            Briket,
            [Description("Prill")]
            Prill,
        }
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
        public enum MessageInfoType
        {
            Success,
            Error,
            Warning,
            Info
        }
    }
}
