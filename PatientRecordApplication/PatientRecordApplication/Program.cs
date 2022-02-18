using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PatientRecordApplication
{
    /// <summary>
    /// struct for the patient record
    /// </summary>
    class Patient
    {
        public int ID = 0;
        public string name = "";
        public decimal due = 0.0m;

        public Patient(int x, string str, decimal d)
        {
            ID = x;
            name = str;
            due = d;
        }

        public void print()
        {
            Console.WriteLine($"ID: {ID}\t\tName: {name}\t\tDue: ${due}");
        }
    };

    /// <summary>
    /// Main Patient class that holds info that is stored on the personalised file
    /// </summary>
    class Records
    {
        //array of records for the file to load into
        //first line of file will be the count of how many records there are
        //count/records will be updated when the file is rewriten
        private Patient[] arr;

        //max ID
        private int ID = 0;

        //ID count
        private int Max = 0;

        
        public void addID()
        {
            //ask for input of name and due
            string str = "";
            decimal bal = 0.0m;

            while (true)
            {
                try
                {
                    Console.WriteLine("Enter name:");
                    str = Console.ReadLine();
                    break;
                }
                catch
                {
                    Console.WriteLine("ERROR: Only String Allowed");
                }
            }
            while(true)
            {
                try
                {
                    Console.WriteLine("Enter Balance Due:");
                    bal = decimal.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("ERROR: Only Decimal Allowed");
                }
            }

            //take max record ID + 3 = new ID
            arr[ID] = new Patient(Max, str, bal);
            Max += 3;
            ID++;
        }


        /// <summary>
        /// w/ ID can find the patient and information
        /// </summary>
        public void editID()
        {
            int id = 0;
            bool changed = true;
            while (changed)
            {
                try
                {
                    Console.WriteLine("Enter ID to Edit");
                    id = int.Parse(Console.ReadLine());

                    bool found = false;

                    for (int i = 0; i < ID; i++)
                    {
                        if (arr[i].ID == id)
                        {
                            found = true;
                            arr[i].print();
                        }
                    }

                    if (found == false)
                    {
                        Console.WriteLine("ERROR: ID not found");
                    }
                    else
                    {
                        int senti = 0;
                        bool edit = true;
                        while (edit)
                        {
                            try
                            {
                                Console.WriteLine("EDIT:\n1: Name\n2: Balance\n3: QUIT");
                                senti = int.Parse(Console.ReadLine());
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("ERROR: Invalid input {only int accepted}\n");
                            }
                        }
                        switch (senti)
                        {
                            case 1:
                                Console.WriteLine("Enter New Name:");
                                try
                                {
                                    arr[id].name = Console.ReadLine();
                                    changed = false;
                                }
                                catch
                                {
                                    Console.WriteLine("ERROR: Invalid input {only string accepted}\n");
                                }
                                break;
                            case 2:
                                Console.WriteLine("Enter New Balance Due:");
                                try
                                {
                                    arr[id].due = decimal.Parse(Console.ReadLine());
                                    changed = false;
                                }
                                catch
                                {
                                    Console.WriteLine("ERROR: Invalid input {only decimal accepted}\n");
                                }
                                break;
                            case 3:
                                edit = false;
                                changed = false;
                                break;
                        }

                    }
                }
                catch
                {
                    Console.WriteLine("ERROR: only int allowed");
                }
            }
        }


        /// <summary>
        /// loads file into array struct to be looked through
        /// </summary>
        public bool readFile(string path)
        {
            Console.WriteLine("Current Patient Files:\n");

            //try to open file
            try
            {
                int count = 0;

                // Read the file and display it line by line.  
                foreach (string line in System.IO.File.ReadLines(@path))
                {
                    //first line information
                    if (count == 0)
                    {
                        string[] temp = line.Split(',');
                        ID = Int16.Parse(temp[0]);
                        Max = Int16.Parse(temp[1]);
                        //set max ID to be doulbe current ammount
                        arr = new Patient[Max*2];
                    }
                    else //rest of file information
                    {
                        //echo
                        //System.Console.WriteLine(line);

                        //loading temp
                        string[] temp = line.Split(',');

                        //asgin new part to arr
                        int id = Int16.Parse(temp[0]);
                        string name = temp[1];
                        decimal due = decimal.Parse(temp[2]);

                        //initalise arr[count - 1]
                        arr[count - 1] = new Patient(id, name, due);
                        arr[count - 1].print();

                        //suspend screen
                        //System.Console.ReadLine();

                        //clear screen
                        //Console.Clear();
                    }
                    count++;
                }
                Console.WriteLine("");
                return true;
            }
            catch (FileNotFoundException fileNotFound)
            {
                Console.WriteLine($"{fileNotFound}\n");
                return false;
            }
        }


        /// <summary>
        /// print class values pulled from the file
        /// print patient values at ID
        /// </summary>
        public void print()
        {
            int id = 0;
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter ID to view");
                    id = int.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("ERROR: only int allowed");
                }
            }
            bool found = false;
            
            for(int i = 0; i < ID; i++)
            {
                if(arr[i].ID == id)
                {
                    found = true;
                    arr[i].print();
                }
            }

            if(found == false)
            {
                Console.WriteLine("ERROR: ID not found");
            }
        }


        /// <summary>
        /// print all
        /// </summary>
        public void printAll()
        {
            Console.WriteLine("Current Patient Files:\n");

            for (int i = 0; i < ID; i++)
            {
                arr[i].print();
            }
        }


        /// <summary>
        /// print class due values equal to or execcding due ammount
        /// </summary>
        public void balanceDue()
        {
            //print patient values >= due
            decimal bal = 0;
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter Min Balances to view");
                    bal = decimal.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("ERROR: only decimal allowed");
                }
            }
            bool found = false;

            for (int i = 0; i < ID; i++)
            {
                //Console.WriteLine(bal + " " + arr[i].due);
                if (arr[i].due <= bal)
                {
                    found = true;
                    arr[i].print();
                }
            }

            if (found == false)
            {
                Console.WriteLine("No Records Below Min Balance Requested");
            }
        }


        /// <summary>
        /// rewrite file to save any changes 
        /// </summary>
        /// <param name="path"></param>
        public void writeFile(string path)
        {
            StreamWriter writer = new StreamWriter(path);

            writer.WriteLine(ID + "," + Max);

            for (int i = 0; i < ID; i++)
            {
                writer.WriteLine(arr[i].ID + "," + arr[i].name + "," + arr[i].due);
            }

            writer.Close();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Records rec = new Records();

            //path to the record file
            string path = @"C:\Users\wdcas\Desktop\CIS 297\cis297-w2022-assignment4-template-wicastle\PatientRecordApplication\PatientRecordApplication\Records.txt";
            rec.readFile(path);

            bool quit = true;
            while (quit)
            {

                int senti = 0;
                try
                {
                    Console.WriteLine("Enter Command:\n0: Print All\n1: Print ID\n2: Edit ID\n3: Add new\n4: Min Balance\n5: QUIT");
                    senti = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("ERROR: Invalid input {only int accepted}\n");
                }

                //clear for new screen to output
                Console.Clear();

                switch (senti)
                {
                    case 0:
                        rec.printAll();
                        break;
                    //print ID
                    case 1:
                        rec.print();
                        break;

                    //edit ID
                    case 2:
                        rec.editID();
                        break;

                    //add ID
                    case 3:
                        rec.addID();
                        break;

                    //min balance due
                    case 4:
                        rec.balanceDue();
                        break;

                    //quit
                    case 5:
                        //write save file
                        rec.writeFile(path);

                        //flip quit value to leave
                        quit = false;
                        break;
                }
            }
            //suspend screen
            //System.Console.ReadLine();
        }
    }
}
