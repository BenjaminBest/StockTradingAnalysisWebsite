using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace StockTradingAnalysis.Web.Migration.Common
{
    public class MigrationItemPersister<TItem>
    {
        public string Identifier { get; private set; }

        public MigrationItemPersister(string identifier)
        {
            Identifier = identifier;
        }

        public void Set(List<TItem> items)
        {
//            File.WriteAllText($"{Identifier}.txt", JsonConvert.SerializeObject(item));


           Stream ms = File.OpenWrite($"{Identifier}.txt");

            var formatter = new BinaryFormatter();
            
            formatter.Serialize(ms, items);
            ms.Flush();
            ms.Close();
            ms.Dispose();

        }

        public List<TItem> Get()
        {
            //var data = File.ReadAllText($"{Identifier}.txt");
            //return JsonConvert.DeserializeObject<List<TItem>>(data);

            var formatter = new BinaryFormatter();

            //Reading the file from the server
            var fs = File.Open($"{Identifier}.txt", FileMode.Open);

            var obj = formatter.Deserialize(fs);

            fs.Flush();
            fs.Close();
            fs.Dispose();

            return (List<TItem>)obj;
        }
    }
}
