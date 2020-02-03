using Npgsql;
using System;
namespace DraftDay {
    public class Player : Person {

        
        private int strength, speed;
        NpgsqlConnection conn;
        public Player(string name, int age, int strength, int speed, int id) : base(name, age, id){
            this.strength = strength;
            this.speed = speed;
        }
        
        public int getStrength(){
            return this.strength;
        }
        public int getSpeed(){
            return this.speed;
        }
        public void setStrength(int strength){
            this.strength = strength;  
        }
        public void setSpeed(int speed){
            this.speed = speed;
        }
        
        // public void setStrength(int strength){
        //     this.strength = strength;
        // }
        // public void setSpeed(int speed){
        //     this.speed = speed;
        // }

        public override string ToString(){
            return "Player(" + getID() + "): " +
                    "\t\t" + getName() +
                    "\t\t\t" + getAge() + 
                    "\t\t" + getStrength() +
                    "\t\t" + getSpeed()
            ;
        }
        
        public string createPlayer(Player player){
            connectDB();
            
            string sql = "INSERT INTO Player" + 
                         "(ID, Name, Age, Strength, Speed)" +
                         "VALUES(" + 
                         getID() + "," +
                          "'" + getName() + "'" + "," +
                         getAge() + "," +
                         getStrength() + "," +
                         getSpeed() + ");";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            return player.getName() + " was created";
        }
        public string updatePlayer(Player player){
            connectDB();
            
            string sql = "UPDATE Player" + 
                         " SET ID = " + getID() +
                             " ,Name = " + "'" + getName() + "'" +
                             " ,Age = " + getAge() + 
                             " ,Strength = " + getStrength() +
                             " ,Speed = " + getSpeed() +
                         "\n WHERE ID = " + getID() + ";";
            
                         
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            return player.getName() + " was updated";
        }
        public string deletePlayer(Player player){
            connectDB();
            
            string sql = "DELETE FROM Player" + 
                         "\n WHERE ID = " + getID() + ";";
            
                         
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            return player.getName() + " was deleted";
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