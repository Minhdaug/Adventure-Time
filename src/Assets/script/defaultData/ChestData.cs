using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.script.model;

namespace Assets.script.defaultData
{
    public class ChestData
    {
        public List<Chest> ChestDefaultList = new List<Chest>() 
        {
            new Chest ()
            {
                id = 0,
                goldReward = 5,
                itemReward = new List<Item>
                {
                    new ItemListData().ShopItems[0],
                    new ItemListData().ShopItems[0],
                    new ItemListData().ShopItems[0],
                },
                opened = false,
            },
            new Chest ()
            {
                id = 1,
                goldReward = 10,
                itemReward = null,
                opened = false,
            },
            new Chest ()
            {
                id = 2,
                goldReward = 0,
                itemReward = new List<Item>
                {
                    new ItemListData().ShopItems[2],
                    new ItemListData().ShopItems[2],
                },
                opened = false,
            },
            new Chest ()
            {
                id = 3,
                goldReward = 20,
                itemReward = null,
                opened = false,
            },
            new Chest ()
            {
                id = 4,
                goldReward = 15,
                itemReward = null,
                opened = false,
            },
            new Chest ()
            {
                id = 5,
                goldReward = 0,
                itemReward = new List<Item>
                {
                    new ItemListData().ShopItems[0],
                    new ItemListData().ShopItems[0],
                    new ItemListData().ShopItems[0],
                    new ItemListData().ShopItems[0],
                },
                opened = false,
            },
            new Chest ()
            {
                id = 6,
                goldReward = 40,
                itemReward = null,
                opened = false,
            },
            new Chest ()
            {
                id = 7,
                goldReward = 30,
                itemReward = null,
                opened = false,
            },
            new Chest ()
            {
                id = 8,
                goldReward = 0,
                itemReward = new List<Item>
                {
                    new ItemListData().ShopItems[1],
                    new ItemListData().ShopItems[1],
                    new ItemListData().ShopItems[1],
                    new ItemListData().ShopItems[1],
                },
                opened = false,
            },
            new Chest ()
            {
                id = 9,
                goldReward = 0,
                itemReward = new List<Item>
                {
                    new ItemListData().ShopItems[3],
                    new ItemListData().ShopItems[3],
                    new ItemListData().ShopItems[3],
                },
                opened = false,
            },
            new Chest ()
            {
                id = 10,
                goldReward = 50,
                itemReward = null,
                opened = false,
            },
            new Chest ()
            {
                id = 11,
                goldReward = 70,
                itemReward = null,
                opened = false,
            },
        };
    }
}
