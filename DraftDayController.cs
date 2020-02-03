using System;
using Npgsql;


namespace DraftDay{


    public class DraftDayController {

       
        private Manager user;

        NpgsqlConnection conn;
    
        private enum Choice {
            Home,
            Draft,
            PlayerCreation,
            Quit
        }
        private Choice response = Choice.Home;


        public void run(){
            connectDB();
            establishTables();
            routing();

            Console.WriteLine("Thanks for using my DraftDay Application" + user.getName());            
        }

        private void routing(){
            do{
                if(response == Choice.Home){
                    Home homePage = new Home(user);
                    decodeResponse(homePage.display());
                    this.user = homePage.getManager();
                }  
                if(response == Choice.Draft){
                    Draft draft =  new Draft(user);
                    decodeResponse(draft.display());
                }
                if( response == Choice.PlayerCreation){
                    PlayerCreation playerCreation = new PlayerCreation();
                    decodeResponse(playerCreation.display());
                }
            }while(response != Choice.Quit );
            
        }
        private void decodeResponse(int encodedResponse){
            if(encodedResponse == 1){
                response = Choice.Home;
            } else if(encodedResponse == 2){
                response = Choice.PlayerCreation;
            } else if(encodedResponse == 3){
                response = Choice.Draft;
            } else if( encodedResponse == -1){
                response = Choice.Quit;
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
        private void establishTables(){
            string sql =  "SELECT * FROM Player;";
            bool tablesCreated = true;

            NpgsqlCommand cmd; 
            try{
                cmd =  new NpgsqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                
            } catch(Exception){
                tablesCreated = false;
            }

            if(tablesCreated){
                
            } else {
               

                sql = "CREATE TABLE PLAYER("    +
                      "ID integer PRIMARY KEY," +
                      "Name varchar(30),"       +
                      "Age integer,"            +
                      "Strength integer,"       +
                      "Speed integer"           +
                ")";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Tables created");
            }

        }
    }
}