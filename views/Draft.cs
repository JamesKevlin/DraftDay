using System;
using System.Collections;
using Npgsql;

namespace DraftDay {

    public class Draft {


        private Manager user;
        ArrayList playerList =  new ArrayList();
        NpgsqlConnection conn;
        public Draft(Manager user){
            this.user = user;
        }

        public int display(){
            int input = -1;
            initPool();
            user.updateDraft();
            Console.WriteLine("Welcome to the Draft");
            

            do{
                Console.WriteLine("Here you're able to draft a team!\n" + 
                              "1. Draft Player\n" +
                              "2. Remove Player\n" + 
                              "3. List your Players\n" +
                              "4. Return to home\n"
                );
                Console.WriteLine("Please Select a menu");
                input = int.Parse(Console.ReadLine());
                
                if(input == 1){
                    draftPlayer();
                }else if(input == 2){
                    removePlayer();
                } else if(input == 3){
                    var draft = user.playerList();
                    Console.WriteLine("ID\t\t\tNAME\t\t\tAGE\t\tSTRENGTH\tSPEED");
                    foreach (Player player in draft)
                    {
                        Console.WriteLine("no");
                        Console.WriteLine(player.ToString());
                    }
                } 

            }while(input != 4);
                input = 1;
           

            return input;
        }
        private void initPool(){
            
            connectDB();
            string sql =  "SELECT * FROM Player;";
            NpgsqlCommand cmd; 
            try{
                cmd =  new NpgsqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();
                while(reader.Read()){
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    int age = reader.GetInt32(2);
                    int strength = reader.GetInt32(3);
                    int speed = reader.GetInt32(4);

                    Player player =  new Player(name, age, strength, speed, id);
                    playerList.Add(player);
                }
                
                
            } catch(Exception e){
                Console.WriteLine(e.StackTrace);
            }
        }
        private Player draftPlayer(){
            string answer;
            int id = -1;
            Player draftee = null;
            
            Console.WriteLine("Enter Player ID");
            answer = Console.ReadLine();

            try {
                id = int.Parse(answer);
            } catch(Exception){

            }
            if(id != -1){
                foreach (Player player in playerList)
                {
                    if(player.getID() == id){
                        draftee = player;
                    }
                }
            }
            if(draftee != null){
                Console.WriteLine(draftee.getName() + " was drafted");
                user.addPlayer(draftee);
            } else {
                Console.WriteLine("That player does not exist");
            }
            
            return draftee;
        }
        private Player removePlayer(){
            string answer;
            int id = -1;
            Player draftee = null;
            
            Console.WriteLine("Enter Player ID");
            answer = Console.ReadLine();

            try {
                id = int.Parse(answer);
            } catch(Exception){

            }
            if(id != -1){
                foreach (Player player in playerList)
                {
                    if(player.getID() == id){
                        draftee = player;
                    }
                }
            }
            if(draftee != null){
                Console.WriteLine(draftee.getName() + " was removed");
                user.removePlayer(draftee);
            } else {
                Console.WriteLine("That player does not exist");
            }
            
            return draftee;
            
        }
        private void connectDB(){

            try{
                string connString = "Host=127.0.0.1;Username=postgres;Password=foxconnit;Database=mydb3";          
                conn = new NpgsqlConnection(connString);
                conn.Open();
            } catch(Exception ex){
                Console.WriteLine(ex.StackTrace);
            }  
            
        }
    }
}