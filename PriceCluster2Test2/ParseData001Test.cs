using PriceCluster2;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PriceCluster2Test2
{
    public class ParseData001Test
    {
        public static string ToDebugString<TKey, TValue>(IDictionary<TKey, TValue> dictionary)
        {
            return "{" + string.Join(",\n", dictionary.Select(kv => "{ \"" + kv.Key + "\", \"" + kv.Value + "\" }").ToArray()) + "}";
        }

        public bool TestDictionaryEquals<T1, T2>(Dictionary<T1, T2> dic1, Dictionary<T1, T2> dic2)
        {
            return dic1.Count == dic2.Count && !dic1.Except(dic2).Any();
        }

        [Fact]
        public void Test1()
        {
            var parseData = new ParseData001("Data/test.html");
            Assert.Equal(43542, parseData.Price);
            var properties = new Dictionary<string, string>
            {
                { "Plattform", "PC" },
                { "Grafikkprosessor", "Radeon HD5450" },
                { "Prosessormodell", "Intel Core i3  540" },
                { "Flash/SSD", "Nei" },
                { "Produktnavn", "HP Elite 7100 VN892EA" },
                { "Produsenter", "HP" },
                { "Kategori", "Stasjonære datamaskiner" },
                { "Hovedkort Chipsett", "" },
                { "Kabinettype", "" },
                { "Merkedatamaskin", "Ja" },
                { "Nettaggregat", "300 W" },
                { "DisplayPort", "" },
                { "DVI", "" },
                { "HDMI", "Ja" },
                { "Antall HDMI-innganger", "" },
                { "Antall HDMI-utganger", "" },
                { "HDMI-versjon", "" },
                { "Thunderbolt", "Nei" },
                { "USB-kontakt", "Ja" },
                { "Type USB-kontakt", "USB (Type A)" },
                { "USB-kontakt (totalt)", "" },
                { "Øvrige tilkoblinger", "Firewire" },
                { "USB 4", "" },
                { "Skjerm", "Nei" },
                { "Farge", "" },
                { "Mål (BxHxD)", "" },
                { "Bredde", "" },
                { "Dybde", "" },
                { "Høyde", "" },
                { "Produktvekt", "10.2 kg" },
                { "Funksjoner", "" },
                { "Inkludert tilbehør", "Tastatur" },
                { "Dedikert grafikkminne", "Ja" },
                { "Grafikkminne", "" },
                { "Type grafikkminne", "" },
                { "Separate skjermkort (ikke integrert)", "" },
                { "Bluetooth", "" },
                { "Ethernet-tilkobling (RJ45)", "Ja" },
                { "Maks Ethernet-hastighet", "" },
                { "Trådløst nettverk", "" },
                { "Harddisk (mekanisk/hybrid)", "Ja" },
                { "Harddiskstørrelse", "500 GB" },
                { "Minnekortleser", "Ja" },
                { "Type minnekort", "" },
                { "Optisk stasjon", "Ja" },
                { "Type optisk stasjon", "DVD-brenner" },
                { "Miljømerking", "Energystar" },
                { "Antall plasser", "4 stk" },
                { "Ledige plasser", "1 stk" },
                { "Maksimal minnestørrelse", "16 GB" },
                { "Minnehastighet", "1333 MHz" },
                { "Minneplass", "DIMM" },
                { "Minnetype", "DDR3" },
                { "RAM- minne", "3 GB" },
                { "32/64-bit OS", "64-bit" },
                { "Operativsystem", "Windows XP Pro" },
                { "Kontakt", "" },
                { "Prosessorhastighet", "3.06 GHz" },
                { "Prosessorkjernetype", "2 (Dual) Core" },
                { "Lanseringsår", "2010" },
                { "LavestePris", "43542" }};

            Assert.True(TestDictionaryEquals(parseData.Properties, properties), ToDebugString(parseData.Properties));
        }
    }
}
