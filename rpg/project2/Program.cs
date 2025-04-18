using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks.Dataflow;
using System.Collections.Generic;


class Character
{
    public string name;
    public string job;
    public int level;
    public int attack;
    public int defense;
    public int hp;
    public int gold;

    public Character(string name, string job)
    {
        this.name = name;
        this.job = job;
        level = 1;
        attack = 5;
        defense = 10;
        hp = 300;
        gold = 1000;

    }

}

class TextRPG
{
    List<string> items = new List<string>();
    List<string> equippedItems = new List<string>();
    List<string> shopItems = new List<string>();

    Character player;

    public static void Main()
    {
        TextRPG game = new TextRPG();
        game.Run();
    }
    public void Run()
    {
        Console.WriteLine("스파르타 던전에 입장하셨습니다.");
        string name = ChoiceName();
        Console.WriteLine($"{name}님 스파르타 던전에 오신 걸 다시 한 번 환영합니다.");
        string job = ChoiceJob();
        Console.WriteLine($"직업이 선택되었습니다 : {job}");
        player = new Character(name, job);
        Item();
        Market();
        StatusWindow();
        Waiting();

    }
    static string ChoiceName()
    {
        while (true)
        {
            Console.WriteLine("사용하실 이름을 입력하세요.");
            string name = Console.ReadLine();

            Console.WriteLine("입력하신 이름은 {0} 입니다.", name);
            Console.WriteLine("저장하시겠습니까?");
            Console.WriteLine("1. 저장");
            Console.WriteLine("2. 취소");

            string number =  Console.ReadLine();

            if (number == "1")
            {
                return name;
            }
            else if (number == "2")
            {
                continue;
            }
            else
            {
                Console.WriteLine("잘못 입력하셨습니다.");
            }
        }
    }

    static string ChoiceJob()
    {   
        while(true)
        {
            string[] job = {"전사", "도적", "마법사"};
            Console.WriteLine("직업을 선택해주세요");
            Console.WriteLine("1. 전사");
            Console.WriteLine("2. 도적");
            Console.WriteLine("3. 마법사");
            int number = int.Parse(Console.ReadLine());

            if(number == 1)
            {
                return job[0];
            }
            else if(number == 2)
            {
                return job[1];
            }
            else if(number == 3)
            {
                return job[2];
            }
            else
            {
                Console.WriteLine("잘못 입력하셨습니다.");
            }
        }
    }

    public void StatusWindow()
    {
        Console.WriteLine("캐릭터의 정보가 표시됩니다.");
        Console.WriteLine();
        Console.WriteLine($"{player.name}");
        Console.WriteLine($"Lv. {player.level}");
        Console.WriteLine($"Chad : {player.job}");
        Console.WriteLine($"공격력 : {player.attack}");
        Console.WriteLine($"방어력 : {player.defense}");
        Console.WriteLine($"체력  : {player.hp}");
        Console.WriteLine($"Gold : {player.gold}");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        while (true)
        {
            string number = Console.ReadLine();

            if (number == "0")
            {
                Waiting();
                break;
            }
            else
            {
                Console.WriteLine("잘못 입력하셨습니다.");
                Console.WriteLine("다시 입력해주세요.");
                continue;
            }
        }
    }

    public void Waiting()
    {
        Console.Clear();
        Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
        Console.WriteLine("1. 상태 보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 상점");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">>");

        while(true)
        {
            string number = Console.ReadLine();
            if(number == "1")
            {
                StatusWindow();
                break;
            }
            else if(number == "2")
            {
                Inventory();
                break;
            }
            else if(number == "3")
            {
                Market();
                break;
            }
            else
            {
                Console.WriteLine("잘못 입력하셨습니다.");
                Console.WriteLine("다시 입력해주세요.");
                continue;
            }
        }
    } 

    public void Inventory()
    {
        Console.WriteLine("[아이템 목록]");
        for (int i = 0; i < items.Count; i++)
        {
            string display = equippedItems.Contains(items[i]) ? $"[E] {items[i]}" : items[i];
            Console.WriteLine($"{i + 1}. {display}");
        }
        Console.WriteLine();
        Console.WriteLine("착용할 아이템 번호를 입력하세요.");
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요");
        Console.Write(">>");

        while(true)
        {   
            string number = Console.ReadLine();
            if (int.TryParse(number, out int index))
            {
    
                if (index == 0)
                {
                    Waiting();
                    break;
                }
                else if (index >= 1 && index <= items.Count)
                {
                    EquipItem(index - 1);
                    Inventory();
                }            
                else
                {
                    Console.WriteLine("잘못 입력하셨습니다.");
                    Console.WriteLine("다시 입력해주세요.");
                    continue;
                }

            }
        }
    }
    void EquipItem(int index)
    {
        string item = items[index];
        if (equippedItems.Contains(item))
        {
            Console.WriteLine($"{item}은(는) 이미 착용 중입니다!");
        }
        else
        {
            Console.WriteLine($"{items[index]}을(를) 착용했습니다!");
            equippedItems.Add(item);
            Inventory();
        }
    }
    public void Item()
    {
        items.Add("무쇠갑옷      | 방어력 +5 | 무쇠로 만들어져 튼튼한 갑옷입니다.");
        items.Add("스파르타의 창  | 공격력 +7 | 스파르타의 전사들이 사용했다는 전설의 창입니다.");
        items.Add("낡은 검      | 공격력 +2 | 쉽게 볼 수 있는 낡은 검 입니다.");
    }

    public void Market()
    {

        Console.WriteLine("[보유골드]");
        Console.WriteLine($"{player.gold}");
        Console.WriteLine();
        Console.WriteLine("[상점 아이템 목록]");

        shopItems.Add("수련자의 갑옷    | 방어력  +5  | 수련에 도움을 주는 갑옷입니다          |  1000G");
        shopItems.Add("수련자의 갑옷    | 방어력  +5  | 수련에 도움을 주는 갑옷입니다          |  1000G");
        shopItems.Add("수련자의 갑옷    | 방어력  +5  | 수련에 도움을 주는 갑옷입니다          |  1000G");
        shopItems.Add("수련자의 갑옷    | 방어력  +5  | 수련에 도움을 주는 갑옷입니다          |  1000G");
        shopItems.Add("수련자의 갑옷    | 방어력  +5  | 수련에 도움을 주는 갑옷입니다          |  1000G");
        shopItems.Add("수련자의 갑옷    | 방어력  +5  | 수련에 도움을 주는 갑옷입니다          |  1000G");
        Console.WriteLine();
        Console.WriteLine("구매할 아이템 번호를 입력해주세요.");
        Console.WriteLine("0. 나가기");
        int number = int.Parse(Console.ReadLine());

        if (number == 0)
        {
            Waiting();
        }
    }
    
}




