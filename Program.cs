using System;
using System.Diagnostics.SymbolStore;
using System.Drawing;

namespace EpiskBlackjack
{
    internal class Program
    {
        int antalKort = 52;
        int ess = 4;
        int tvåor = 4;
        int treor = 4;
        int fyror = 4;
        int femmor = 4;
        int sexor = 4;
        int sjuor = 4;
        int åttor = 4;
        int nior = 4;
        int tior = 4;
        int knäktar = 4;
        int drottningar = 4;
        int kungar = 4;

        int gogogaga;

        string dealernsKort;
        int dealerpoäng = 0;
        int spelarPoäng;
        int slumpatTal;
        Random random = new Random();
        string mottagetKort;

        float pengar = 1000;
        float satsadePengar;
        float extraSatsning = 0;
        float minSatsning = 250;
        bool giltigtSvar = false;
        float minSatsningMultiplikator = 1;










        //Detta är spelarens tur
        void spelarensTur()
        {
            spelarPoäng = 0;
            bool spel = true;
            string svar;
            int försök = 2;
            Program p = new Program();

            while (spel)
            {


                //Loop för antal gånger man ska dra ett kort. Använder det bara för att dra 2 kort i början.
                for (int i = 0; i < försök; i++)
                {



                    //Kallar på funktionen som drar ett kort (det blir lättare en att ha ett stort block med kod här istället)
                    p.draEttKort();

                    //Kollar om kortet är ett ess så man får välja hur många poäng man vill ha.
                    if (p.mottagetKort == "ess")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("du fick ett ess");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Vill du ha 1 eller 11 poäng?");



                        //While loop som ser till att man ger ett giltigt svar
                        svar = Console.ReadLine();

                        while (svar != "11" && svar != "1")
                        {

                            Console.WriteLine("Du måste svara '1' eller '11'");
                            svar = Console.ReadLine();

                        }

                        //Lägger till poängen
                        spelarPoäng += int.Parse(svar);
                    }



                    //Kollar om man fått ett klätt kort
                    else if (p.mottagetKort == "kung" || p.mottagetKort == "drottning" || p.mottagetKort == "knäkt")
                    {

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("du fick en " + p.mottagetKort);
                        Console.ForegroundColor = ConsoleColor.White;
                        spelarPoäng += 10;

                    }



                    //Om man inte fått ett ess eller klätt kort
                    else
                    {

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("du fick en " + p.mottagetKort + ":a");
                        Console.ForegroundColor = ConsoleColor.White;
                        spelarPoäng += int.Parse(p.mottagetKort);

                    }



                }
                //Här slutar kortdragningen





                //Kollar om man får fortsätta dra
                if (spelarPoäng < 21)
                {

                    Console.WriteLine("Du har " + spelarPoäng + " poäng, vill du fortsätta ja eller nej?");

                    //While loop som ser till att man ger ett giltigt svar
                    svar = Console.ReadLine().ToLower().Trim();

                    while (svar != "ja" && svar != "nej" && svar != "j" && svar != "n")
                    {
                        Console.WriteLine("Du måste svara 'ja' eller 'nej'");
                        svar = Console.ReadLine().ToLower().Trim();
                    }



                    //När man svarar ja
                    if (svar == "ja" || svar == "j")
                    {
                        försök = 1;
                    }



                    //om man inte svarar ja (nej)
                    else
                    {
                        spel = false;
                    }



                }



                //kollar om man har 21
                else if (spelarPoäng == 21)
                {
                    spel = false;
                    Thread.Sleep(1000);
                }



                //kollar om man blivit tjock
                else if (spelarPoäng > 21)
                {
                    Thread.Sleep(1000);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Du blev tjock!");
                    Console.ForegroundColor = ConsoleColor.White;
                    spel = false;
                    Thread.Sleep(1000);
                }



            }
        }










        //det mesta här är kopierat från spelar-koden
        void dealernsTur()
        {
            dealerpoäng = 0;
            Program p = new Program();
            bool spel = true;
            int försök = 2;
            dealernsKort = "Mina kort var: ";

            while (spel)
            {


                //Loop för antal gånger man ska dra ett kort. Använder det bara för att dra 2 kort i början.
                for (int i = 0; i < försök; i++)
                {



                    //Kallar på funktionen som drar ett kort (det blir lättare en att ha ett stort block med kod här istället)
                    p.draEttKort();
                    dealernsKort += p.mottagetKort + ", ";

                    //Kollar om kortet är ett ess så man får välja hur många poäng man vill ha.
                    if (p.mottagetKort == "ess")
                    {

                        //Om dealern kan ta 11 poäng gör han det
                        if (dealerpoäng + 11 < 22)
                        {
                            dealerpoäng += 11;
                        }
                        else
                        {
                            dealerpoäng += 1;
                        }
                    }



                    //Kollar om man fått ett klätt kort
                    else if (p.mottagetKort == "kung" || p.mottagetKort == "drottning" || p.mottagetKort == "knäkt")
                    {

                        dealerpoäng += 10;

                    }



                    //Om man inte fått ett ess eller klätt kort
                    else
                    {

                        dealerpoäng += int.Parse(p.mottagetKort);

                    }



                }
                //Här slutar kortdragningen





                //Kollar om dealern ska fortsätta dra
                if (dealerpoäng < 18)
                {

                    försök = 1;

                }



                else
                {

                    spel = false;

                }

            }
        }









        //Om man har valt spelläge 0 körs denna kod innan varje runda
        void satsa()
        {
            string svar;

            Console.WriteLine("Hur mycket pengar vill du satsa från " + minSatsning.ToString() + " till " + pengar.ToString());



            //While loop som ser till att man ger ett giltigt svar
            svar = Console.ReadLine();
            giltigtSvar = float.TryParse(svar, out satsadePengar);

            while (giltigtSvar == false)
            {

                Console.WriteLine("Du måste ge ett giltigt svar");
                svar = Console.ReadLine();
                giltigtSvar = float.TryParse(svar, out satsadePengar);

            }



            //En clamp så man inte kan satsa mer en man har eller mindre en man får
            satsadePengar = Math.Clamp(satsadePengar, minSatsning, pengar);

            Console.WriteLine();
            Console.WriteLine("Du har satsat " + satsadePengar + " pengar");

            //Gör så att man behöver satsa mer efter varje runda
            minSatsningMultiplikator += 0.05f;
            minSatsning = MathF.Round(minSatsning * minSatsningMultiplikator);

            //Tar bort pengar och lägger till det i penga-poolen
            pengar -= satsadePengar;
        }









        //Main
        static void Main(string[] args)
        {
            string svar;
            Program p = new Program();
            string spelLäge = "0";

            int vinster = 0;
            int dealerVinster = 0;
            int harVunnit = 0;
            bool spel = true;


            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Välkommen till episk blackjack");
            Console.WriteLine("Vill du köra klassiskt, Skriv 0");
            Console.WriteLine("Vill du köra oändligt, skriv 1");
            Console.WriteLine("Vill du köra episkt läge, skriv 2");

            svar = Console.ReadLine();
            //While loop som ser till att man ger ett giltigt svar
            while (svar != "0" && svar != "1" && svar != "2")
            {

                Console.WriteLine("Du måste ge ett giltigt svar ('0', '1' eller '2')");
                svar = Console.ReadLine();

            }

            //Om man svarar 0 (klassiskt läge)
            if (svar == "0")
            {
                spelLäge = "0";
                Console.WriteLine("Du har valt klassiskt läge, om du för slut på pengar, förlorar du. Det blir svårare med tiden.");
                Console.WriteLine("Tryck enter för att fortsätta");
                Console.ReadLine();
                p.satsa();
            }

            //Om man svarar 1 (oändligt läge)
            else if (svar == "1")
            {
                spelLäge = "1";
                Console.WriteLine("Du har valt oändligt läge");
                Console.WriteLine("(Tryck enter för att fortsätta)");
                Console.ReadLine();
            }

            //Om man svarar något annat (2 aka episkt läge)
            else
            {
                spelLäge = "2";
                Console.WriteLine("Du har valt episkt läge, om du förlorar börjar du om");
                Console.WriteLine("(Tryck enter för att fortsätta)");
                Console.ReadLine();
            }





            //spel är variabeln som bestämmer om man får fortsätta spela eller inte
            while (spel)
            {

                //Kallar på spelar-funktionen
                p.spelarensTur();

                //visar resultaten från spelarens tur
                //om man fått mer en 21 poäng blir texten röd
                if (p.spelarPoäng > 21)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                //annars blir den grön
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }

                //Skriver ens poäng
                Console.WriteLine("du fick totalt " + p.spelarPoäng + " poäng");
                Console.ForegroundColor = ConsoleColor.White;

                Thread.Sleep(1000);

                //Nu är det dealerns tur😨
                p.dealernsTur();

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Blue;
                //variabeln "dealernskort" är "jag fick" plus en lista på dealerns kort
                Console.WriteLine(p.dealernsKort);
                Console.WriteLine("Det blir " + p.dealerpoäng + " poäng");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("");

                Thread.Sleep(1000);

                //Kollar resultaten och ser vem som vunnit
                //När variabeln "harVunnit" är 0 har man förlorat, när den är 1 har man vunnit och när den är 2 är det oavgjort
                if (p.dealerpoäng > 21)
                {
                    if (p.spelarPoäng > 21)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Vi båda blev tjocka, därför är det oavgjort");
                        harVunnit = 2;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Jag blev tjock, därför vinner du");
                        harVunnit = 1;
                    }
                }
                else if (p.spelarPoäng > 21)
                {
                    if (p.dealerpoäng > 21)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Vi båda blev tjocka, därför är det oavgjort");
                        harVunnit = 2;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Du blev tjock, därför vinner jag");
                        harVunnit = 0;
                    }
                }
                else
                {
                    if (p.spelarPoäng > p.dealerpoäng)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Du fick mer poäng en mig, därför vinner du");
                        harVunnit = 1;
                    }
                    else if (p.spelarPoäng == p.dealerpoäng)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Vi fick lika många poäng, därför blir det oavgjort");
                        harVunnit = 2;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Jag fick mer poäng en dig, därför vinner jag");
                        harVunnit = 0;
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(1000);





                //Om man kör klassiskt läge kommer denna kod köras innan loopen början om (mest betting grejer)
                if (spelLäge == "0")
                {
                    vinster += 1;

                    //Om man vunnit
                    if (harVunnit == 1)
                    {
                        p.pengar += (p.satsadePengar * 2);

                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Grattis! du vann");

                        Console.WriteLine("Du har vunnit " + (p.satsadePengar * 2) + " pengar och har nu " + p.pengar + " totalt.");
                        Console.ForegroundColor = ConsoleColor.White;

                        p.satsadePengar = 0;

                        //Om man kan fortsätta köra
                        if (p.pengar > p.minSatsning)
                        {
                            Console.WriteLine("(Tryck enter för att fortsätta)");
                            Console.ReadLine();
                            p.satsa();
                        }
                        //Om man har mindre pengar en bettinggränsen
                        else
                        {
                            spel = false;
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Ojdå! det ser ut som att du inte har nog med pengar för att fortsätta");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Du klarade dig till runda " + vinster);

                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("(Tryck enter för att fortsätta)");
                            Console.ReadLine();
                        }
                    }



                    //Om man har förlorat
                    else if (harVunnit == 0)
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ojdå, du förlorade och har nu " + p.pengar + " pengar");
                        Console.ForegroundColor = ConsoleColor.White;

                        p.satsadePengar = 0;

                        //Om man kan fortsätta köra
                        if (p.pengar > p.minSatsning)
                        {
                            Console.WriteLine("(Tryck enter för att fortsätta)");
                            Console.ReadLine();
                            p.satsa();
                        }
                        //Om man har mindre pengar en bettinggränsen
                        else
                        {
                            spel = false;

                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Ojdå! det ser ut som att du inte har nog med pengar för att fortsätta");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Du klarade dig till runda " + vinster);

                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("(Tryck enter för att fortsätta)");
                            Console.ReadLine();
                        }
                    }



                    //Om det blivit oavgjort
                    else
                    {
                        p.pengar += p.satsadePengar - p.extraSatsning;
                        p.satsadePengar = 0;

                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine();
                        Console.WriteLine("Du har fått tillbaka dina pengar och har nu " + p.pengar);
                        Console.ForegroundColor = ConsoleColor.White;

                        //Om man kan fortsätta köra
                        if (p.pengar > p.minSatsning)
                        {
                            Console.WriteLine("(Tryck enter för att fortsätta)");
                            Console.ReadLine();
                            p.satsa();
                        }
                        //Om man har mindre pengar en bettinggränsen
                        else
                        {
                            spel = false;

                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Ojdå! det ser ut som att du inte har nog med pengar för att fortsätta");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Du klarade dig till runda " + vinster);

                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("(Tryck enter för att fortsätta)");
                            Console.ReadLine();
                        }
                    }
                }






                //Om man kör oändligt läge körs den här koden istället
                else if (spelLäge == "1")
                {
                    //Om man vunnit
                    if (harVunnit == 1)
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Grattis! du vann");

                        Console.ForegroundColor = ConsoleColor.White;
                        vinster += 1;
                        Console.WriteLine("Just nu har du " + vinster + " vinster och jag har " + dealerVinster);

                        Console.WriteLine("(Tryck enter för att fortsätta)");
                        Console.ReadLine();
                    }
                    //Om man förlorat
                    else if (harVunnit == 0)
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ojdå, du förlorade");

                        Console.ForegroundColor = ConsoleColor.White;
                        dealerVinster += 1;
                        Console.WriteLine("Just nu har du " + vinster + " vinster och jag har " + dealerVinster);

                        Console.WriteLine("(Tryck enter för att fortsätta)");
                        Console.ReadLine();
                    }
                    //Om det blivit oavgjort
                    else
                    {
                        Console.WriteLine("Just nu har du " + vinster + " vinster och jag har " + dealerVinster);
                        Console.WriteLine("(Tryck enter för att fortsätta)");
                        Console.ReadLine();
                    }
                }






                //Om man kör episkt läge körs den här koden
                else
                {
                    //Om man vunnit
                    if (harVunnit == 1)
                    {
                        Console.WriteLine("Grattis! du vann (Tryck enter för att fortsätta)");
                        vinster += 1;
                        Console.ReadLine();
                    }
                    //Om man förlorat
                    else if (harVunnit == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine();
                        Console.WriteLine("Hoppsan! du förlorade");

                        spel = false;

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Du vann " + vinster + " gånger utan att förlora, häftigt!");

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadLine();
                    }
                    //Om det blivit oavgjort
                    else
                    {
                        Console.WriteLine("Eftersom det blev oavgjort fortsätter det som vanligt, men du fick ingen vinst (Tryck enter för att fortsätta)");
                        Console.ReadLine();
                    }
                }



                //blandakort är en funktion som lägger tillbaka alla kort i korthögen
                p.blandaKort();
            }
        }





        //Funktionen som blandar korthögen
        void blandaKort()
        {
            antalKort = 52;
            ess = 4;
            tvåor = 4;
            fyror = 4;
            femmor = 4;
            sexor = 4;
            sjuor = 4;
            åttor = 4;
            nior = 4;
            tior = 4;
            knäktar = 4;
            drottningar = 4;
            kungar = 4;
        }






        //Funktionen som drar ett kort
        void draEttKort()
        {
            //jag använder en for loop så att koden kan stoppas mitt i (vet inte hur man gör det på ett annat sätt)
            for (int i = 0; i < 1; i++)
            {

                antalKort -= 1;
                gogogaga = 0;
                slumpatTal = random.Next(1, antalKort);


                gogogaga += ess;

                if (slumpatTal < gogogaga)
                {
                    //få ett ess
                    mottagetKort = "ess";
                    ess -= 1;
                    continue;
                }

                gogogaga += tvåor;

                if (slumpatTal < gogogaga)
                {
                    //få en tvåa
                    mottagetKort = "2";
                    tvåor -= 1;
                    continue;
                }

                gogogaga += treor;

                if (slumpatTal < gogogaga)
                {
                    //få en trea
                    mottagetKort = "3";
                    treor -= 1;
                    continue;
                }

                gogogaga += fyror;

                if (slumpatTal < gogogaga)
                {
                    //få en fyra
                    mottagetKort = "4";
                    fyror -= 1;
                    continue;
                }

                gogogaga += femmor;

                if (slumpatTal < gogogaga)
                {
                    //få en femma
                    mottagetKort = "5";
                    femmor -= 1;
                    continue;
                }

                gogogaga += sexor;

                if (slumpatTal < gogogaga)
                {
                    //få en sexa
                    mottagetKort = "6";
                    sexor -= 1;
                    continue;
                }

                gogogaga += sjuor;

                if (slumpatTal < gogogaga)
                {
                    //få en sjua
                    mottagetKort = "7";
                    sjuor -= 1;
                    continue;
                }

                gogogaga += åttor;

                if (slumpatTal < gogogaga)
                {
                    //få en åtta
                    mottagetKort = "8";
                    åttor -= 1;
                    continue;
                }

                gogogaga += nior;

                if (slumpatTal < gogogaga)
                {
                    //få en nia
                    mottagetKort = "9";
                    nior -= 1;
                    continue;
                }

                gogogaga += tior;

                if (slumpatTal < gogogaga)
                {
                    //få en tia
                    mottagetKort = "10";
                    tior -= 1;
                    continue;
                }

                gogogaga += knäktar;

                if (slumpatTal < gogogaga)
                {
                    //få en knäkt
                    mottagetKort = "knäkt";
                    knäktar -= 1;
                    continue;
                }

                gogogaga += drottningar;

                if (slumpatTal < gogogaga)
                {
                    //få en drottning
                    mottagetKort = "drottning";
                    drottningar -= 1;
                    continue;
                }

                gogogaga += kungar;

                if (slumpatTal < gogogaga)
                {
                    //få en kung
                    mottagetKort = "kung";
                    kungar -= 1;
                    continue;
                }

            }
        }








    }
}

