using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Item it = new Item();

            // Khoi tao 10 item ngau nhien
            Item[] tempItems = new Item[10];

            for (int i = 0; i < tempItems.Length; i++)
            {
                Item item = GameUtilities.GetRandomItem();
                if (item != null)
                    tempItems[i] = item;

                System.Threading.Thread.Sleep(100);
            }

            // In ra danh sach item ngau nhien

            Console.WriteLine("Danh sach item ngau nhien: ");
            GameUtilities.ShowItems(tempItems);

            // Dua cac item hop le vao kho do

            Inventory inventory = new Inventory();

            foreach (var item in tempItems)
            {
                if (inventory.ContainsItem(item))
                {
                    if (item.Price > inventory.GetItemByKey(item.ItemName).Price)
                    {
                        inventory.StoreItem(item);
                    }
                    else
                    {
                        inventory.AddGold(item.Price);
                    }
                }
                else
                {
                    inventory.StoreItem(item);
                } 
            }

            // Ban tat ca cac vat pham weapon co pham la Epic va Rare

            List<Item> tempInventoryItems = new List<Item>();

            foreach (KeyValuePair<string, Item> kvalue in inventory.items)
            {
                tempInventoryItems.Add(kvalue.Value);
            }

            for (int i = 0; i < tempInventoryItems.Count; i++)
            {
                Item item = tempInventoryItems[i];
                if (item is Weapon result)
                {
                    if (result.ItemQuality == Weapon.Quality.Epic ||
                        result.ItemQuality == Weapon.Quality.Rare)
                    {
                        inventory.SellItem(item);
                    }
                }
            }

            ItemCompare compare = new ItemCompare();

            tempInventoryItems = new List<Item>();

            foreach (KeyValuePair<string, Item> kvalue in inventory.items)
            {
                tempInventoryItems.Add(kvalue.Value);
            }

            tempInventoryItems.Sort(compare);


            // In ra danh sach cac item co trong kho do
            Console.WriteLine("=============================");
            Console.WriteLine("Danh sach cac item trong kho do: ");

            inventory.ShowAllItem(tempInventoryItems);

            Console.ReadKey();
        }

        private static bool CanStore(Item item)
        {
            return (item is Weapon) || (item is Cloth && item.Price >= 10);
        }
    }


}
