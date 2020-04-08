using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace GearCatalog
{
    public class Database
    {
        private MySqlConnection Connect()
        {
            string connStr = "server=localhost;user=root;database=climbing_gear;port=3306;password=#iAmRoot";
            return new MySqlConnection(connStr);
        }

        public int InsertGear(Gear gear)
        {
            MySqlConnection conn = Connect();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            conn.Open();

            cmd.CommandText =
                "INSERT INTO gear(" +
                "name, category_id, description, brand, weight_grams, length_mm, width_mm, depth_mm, locking" +
                ") VALUES (" +
                "@Name, @Category_id, @Description, @Brand, @Weight_grams, @Length_mm, @Width_mm, @Depth_mm, @Locking)";
            cmd.Parameters.AddWithValue("@Name", gear.Name);
            cmd.Parameters.AddWithValue("@Category_id", gear.CategoryId);
            cmd.Parameters.AddWithValue("@Description", gear.Description);
            cmd.Parameters.AddWithValue("@Brand", gear.Brand);
            cmd.Parameters.AddWithValue("@Weight_grams", gear.WeightGrams);
            cmd.Parameters.AddWithValue("@Length_mm", gear.LengthMM);
            cmd.Parameters.AddWithValue("@Width_mm", gear.WidthMM);
            cmd.Parameters.AddWithValue("@Depth_mm", gear.DepthMM);
            cmd.Parameters.AddWithValue("@Locking", gear.Locking);

            cmd.ExecuteNonQuery();
            
            cmd.CommandText = "SELECT LAST_INSERT_ID()";
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            int newGearId = rdr.GetInt32(0);

            return newGearId;

        }

        public List<Gear> ReadGear()
        {
            MySqlConnection conn = Connect();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            conn.Open();

            cmd.CommandText = "SELECT gear_id, category_id, name, description, brand, weight_grams, length_mm," +
                " width_mm, depth_mm, locking FROM gear";
            MySqlDataReader rdr = cmd.ExecuteReader();

            List<Gear> NewGearList = new List<Gear>();

            while (rdr.Read())
            {
                Gear NewGear = new Gear();
                NewGear.GearId = rdr.GetInt32(0);
                NewGear.CategoryId = rdr.GetInt32(1);
                NewGear.Name = rdr.GetString(2);
                NewGear.Description = rdr.GetString(3);
                NewGear.Brand = rdr.GetString(4);
                NewGear.WeightGrams = rdr.GetInt32(5);
                NewGear.LengthMM = rdr.GetInt32(6);
                NewGear.WidthMM = rdr.GetInt32(7);
                NewGear.DepthMM = rdr.GetInt32(8);
                NewGear.Locking = rdr.GetInt16(9);

                NewGearList.Add(NewGear);
            }

            return NewGearList;
        }

        public void RemoveGear(IEnumerable<Gear> gear)
        {
            List<Gear> newGearList = new List<Gear>(gear);
            MySqlConnection conn = Connect();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandText = "DELETE FROM gear WHERE gear_id IN (@GearId)";

            foreach (Gear element in newGearList)
            { 
                cmd.Parameters.AddWithValue("@GearId", element.GearId);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
        }

        public void EditGear(Gear gear)
        {
            MySqlConnection conn = Connect();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandText = "UPDATE gear SET name = @Name, description = @Description, brand = @Brand, weight_grams = @WeightGrams, length_mm = @LengthMM, " +
                "width_mm = @WidthMM, depth_mm = @DepthMM, locking = @Locking WHERE gear_id = @GearId";
            
            cmd.Parameters.AddWithValue("@Name", gear.Name);
            cmd.Parameters.AddWithValue("@Description", gear.Description);
            cmd.Parameters.AddWithValue("@Brand", gear.Brand);
            cmd.Parameters.AddWithValue("@WeightGrams", gear.WeightGrams);
            cmd.Parameters.AddWithValue("@LengthMM", gear.LengthMM);
            cmd.Parameters.AddWithValue("@WidthMM", gear.WidthMM);
            cmd.Parameters.AddWithValue("@DepthMM", gear.DepthMM);
            cmd.Parameters.AddWithValue("@Locking", gear.Locking);
            cmd.Parameters.AddWithValue("@GearId", gear.GearId);

            cmd.ExecuteNonQuery();
        }
    }
}