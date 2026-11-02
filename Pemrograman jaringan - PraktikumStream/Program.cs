using System.Text.Json;
using System.Xml.Serialization;

namespace PraktikumStream
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Objek asli di memori
            Mahasiswa mhs = new Mahasiswa(
                "24131310014",
                "Angga",
                "Teknologi Informasi",
                new List<string> { "Pemrograman Jaringan", "Pemrograman Mobile", "UI/UX" }
            );

            Console.WriteLine("=== OBJEK ASLI ===");
            mhs.TampilkanInfo();

            // JSON
            var opsiJson = new JsonSerializerOptions { WriteIndented = true };

            // Serialisasi: objek -> teks JSON
            string jsonString = JsonSerializer.Serialize(mhs, opsiJson);
            Console.WriteLine("\n=== HASIL SERIALISASI JSON ===");
            Console.WriteLine(jsonString);

            File.WriteAllText("mahasiswa.json", jsonString);
            string jsonDariFile = File.ReadAllText("mahasiswa.json");

            // Deserialisasi: teks JSON -> objek
            Mahasiswa? hasilJson = JsonSerializer.Deserialize<Mahasiswa>(jsonDariFile);
            Console.WriteLine("\n=== HASIL DESERIALISASI JSON ===");
            hasilJson?.TampilkanInfo();

            // XML
            var serializerXml = new XmlSerializer(typeof(Mahasiswa));

            // Serialisasi: objek -> teks XML
            string xmlString;
            using (var writer = new StringWriter())
            {
                serializerXml.Serialize(writer, mhs);
                xmlString = writer.ToString();
            }
            Console.WriteLine("\n=== HASIL SERIALISASI XML ===");
            Console.WriteLine(xmlString);

            File.WriteAllText("mahasiswa.xml", xmlString);
            string xmlDariFile = File.ReadAllText("mahasiswa.xml");

            // Deserialisasi: teks XML -> objek
            Mahasiswa? hasilXml;
            using (var reader = new StringReader(xmlDariFile))
            {
                hasilXml = serializerXml.Deserialize(reader) as Mahasiswa;
            }
            Console.WriteLine("\n=== HASIL DESERIALISASI XML ===");
            hasilXml?.TampilkanInfo();
        }
    }
}