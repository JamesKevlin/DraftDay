using System.Collections;
using System;
using Npgsql;

namespace DraftDay{
    public class Manager : Person{

        private string name;
        private int age, id;
        private ArrayList draft;
        NpgsqlConnection conn;
        public Manager(string name, int age, int id, ArrayList draft) : base(name, age, id){
            this.name = name;
            this.age = age;
            this.id = id;
            this.draft =  draft;
        }

        public Player addPlayer(Player player){
           draft.Add(player);
           return player;
        }
        public Player removePlayer(Player player){
            draft.Remove(player);
            return player;
        }

        public ArrayList playerList(){
            ArrayList tempList = new ArrayList();

            foreach(Player p in draft){
                tempList.Add(p);
            }
            return tempList;
        }

        // Overloaded
        
        public Player getPlayer(string name){
            Player player = null;
            foreach (Player p in draft)
            {
                if(string.Equals(name,p.getName())){
                    player = p;
                }
            }
            return player;
        }
        public Player getPlayer(int id){
            Player player = null;
            foreach (Player p in draft)
            {
                if(string.Equals(id,p.getID())){
                    player = p;
                }
            }
            return player;
        }

        public void updateDraft(){
            ArrayList dbPool = new ArrayList();
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
                    dbPool.Add(player);
                }
               
                
            } catch(Exception e){
                Console.WriteLine(e.StackTrace);
            }

            // compare lists

            foreach (Player p in draft)
            {
                int id = p.getID();
                int remove = -1;
                foreach (Player dbP in dbPool)
                {
                    if(dbP.getID() == p.getID()){
                        remove = 0;
                    }
                }
                // if remove is 0 that means player is found
                if(remove == -1){
                    removePlayer(p);
                }
            }

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