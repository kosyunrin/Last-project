using System.IO;
using System.Text;
using UnityGameFramework.Runtime;

namespace GameName
    {

    public enum ItemType
    {
        Human,
        animals,
        robot,
        Chimera
    }
    public enum ItemQuality
    {
        N,
        R,
        SR,
        SSR,
        UR
    }
    public enum BodyType
    {
        None,
        MainPart,
        OtherPart,
    }
    public class DRMyPackData : DataRowBase
    {


        private int m_Id = 0;

        public override int Id
        {
            get
            {
                return m_Id;
            }
        }
        public string Name { get; private set; }
        public int Quality { get; private set; }
        public int Type { get; private set; }
        public int CardType { get; private set; }
        public int Description { get; private set; }
        public int Capacity { get; private set; }
        public int BuyPrice { get; private set; }
        public int Sellprice { get; private set; }



        public override bool ParseDataRow(string dataRowString, object userData)
        {
            string[] columnStrings = dataRowString.Split(DataTableExtension.DataSplitSeparators);
            for (int i = 0; i < columnStrings.Length; i++)
            {
                columnStrings[i] = columnStrings[i].Trim(DataTableExtension.DataTrimSeparators);
            }

            int index = 0;
            index++;
            m_Id = int.Parse(columnStrings[index++]);
            index++;
            Name = columnStrings[index++];
            Quality = int.Parse(columnStrings[index++]);
            Type = int.Parse(columnStrings[index++]);
            CardType = int.Parse(columnStrings[index++]);
            Description = int.Parse(columnStrings[index++]);
            Capacity = int.Parse(columnStrings[index++]);
            BuyPrice = int.Parse(columnStrings[index++]);
            Sellprice = int.Parse(columnStrings[index++]);

            GeneratePropertyArray();
            return true;
        }

        public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
        {
            using (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    m_Id = binaryReader.Read7BitEncodedInt32();
                    Name = binaryReader.ReadString();
                    Quality = binaryReader.Read7BitEncodedInt32();
                    Type = binaryReader.Read7BitEncodedInt32();
                    CardType = binaryReader.Read7BitEncodedInt32();
                    Description = binaryReader.Read7BitEncodedInt32();
                    Capacity = binaryReader.Read7BitEncodedInt32();
                    BuyPrice = binaryReader.Read7BitEncodedInt32();
                    Sellprice = binaryReader.Read7BitEncodedInt32();
                }
            }

            GeneratePropertyArray();
            return true;
        }
        private void GeneratePropertyArray()
        {

        }
    }
}
