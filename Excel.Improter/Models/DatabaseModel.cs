using System;

namespace Excel.Improter.Models
{
    public class DatabaseModel
    {
        public int Id { get; set; }
        public string YataginInzibatiErazisi { get; set; }
        public string YataginKodu { get; set; }
        public string YataginAdi { get; set; }
        public string SaheninKodu { get; set; }
        public string SaheninAdi { get; set; }
        public string DMAANomresi { get; set; }
        public DateTime? DMAABitmeTarix { get; set; }
        public DateTime? DMAAQeydiyyatTarixi { get; set; }
        public string SenayeMenimsenilmesiSeviyyesi { get; set; }
        public string BalansEhtiyyatlari2019 { get; set; }
        public string Hasilat { get; set; }
        public string HasilatZamaniItkiler { get; set; }
        public string Kesfiyyat { get; set; }
        public string YenidenQiymetlendirme { get; set; }
        public string MoteberliyiTesdiqlenmeyen { get; set; }
        public string SerhedlerinDeyishmesiVeDiger { get; set; }
        public string QaliqEhtiyyatlari2020 { get; set; }
        public string TesdiqEdilmishBalansCemi { get; set; }
        public string Koordinat { get; set; }
        public string MineralXammalBazasiBerpasi { get; set; }
        public string TesdiqOlunmaseBarede { get; set; }
        public string Serh { get; set; }
        public string EhtiyyatinKategoryasi { get; set; }
        public string IstisimarVeziyyeti { get; set; }
        public string IlkinEhtiyyat { get; set; }
        public string IlUzreHasilatCemi { get; set; }
        public string HasilatQaliqlari { get; set; }
        public string FaydaliQazintiNovu { get; set; }
        public string AyrilanSahe { get; set; }
        public string VOEN { get; set; }
        public string MilliGealojiKeshfiyyat { get; set; }
        public string MedaxilVeziyyeti { get; set; }
    }
}
