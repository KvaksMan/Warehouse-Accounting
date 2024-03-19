using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WarehouseAccounting
{
    public class Database
    {
        private MySqlConnection con;

        private List<WarehouseRecordGeneral> records;

        public Database()
        {
            this.records = new List<WarehouseRecordGeneral>();

            Connect();
            Refresh();
        }

        private void Connect()
        {
            string con_string = $"server={Constants.MYSQL.HOST};uid={Constants.MYSQL.USER};pwd={Constants.MYSQL.PSWD};database={Constants.MYSQL.NAME}";
            con = new MySqlConnection(con_string);
            con.Open();
        }

        private void Refresh()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT `id`, `type`, `name`, `count`, `location`, `data` FROM `warehouse`", con);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    this.records.Add(new WarehouseRecordGeneral(
                            dr.GetInt16("id"), dr.GetString("name"), dr.GetInt16("count"), dr.GetInt32("location"),
                            JsonDocument.Parse(dr.GetString("data"))
                        ));

                    for (int i = 0; i < dr.FieldCount; i++)
                        Console.WriteLine($"{dr.GetName(i)}\t{dr[i].GetType()}\t{dr[i].ToString()}");
                    Console.WriteLine("");
                }
                dr.Close();
            } catch (MySqlException ex)
            {
                Console.Error.WriteLine(ex.ToString());
            }
        }

        public void AddRecord(WarehouseRecordGeneral record)
        {
            records.Add(record);

            try
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO `warehouse` (`id`, `type`, `name`, `count`, `location`, `data`)"+
                                                    $"VALUES ('{record.id}', '{record.type}', '{record.name}', '{record.count}', '{record.location}', '{record.GetDataJson()}')", con);
                cmd.ExecuteNonQuery();
            } catch (MySqlException ex)
            {
                Console.Error.WriteLine(ex.ToString());
            }
        }

        public void UpdateRecord_Count(WarehouseRecordGeneral record)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand($"UPDATE `warehouse` SET `count` = '{record.count}' WHERE `warehouse`.`id` = {record.id};", con);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.Error.WriteLine(ex.ToString());
            }
        }

        public WarehouseRecordGeneral[] GetRecords()
        {
            return this.records.ToArray();
        }

        public WarehouseRecordMCU[] GetRecordsMCU()
        {
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return this.records.ToArray()
                                .Where(element => element != null)
                                .Select(element =>
                                {
                                    try
                                    {
                                        return (WarehouseRecordMCU)element;
                                    }
                                    catch (InvalidCastException ex)
                                    {
                                        Console.WriteLine($"Error casting element: {ex.Message}");
                                        return null;
                                    }
                                })
                                .Where(castedElement => castedElement != null)
                                .ToArray();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }

        public WarehouseRecordGeneral GetRecordByID(short id)
        {
            return this.records.Find(record => record.id == id);
        }
    }
}
