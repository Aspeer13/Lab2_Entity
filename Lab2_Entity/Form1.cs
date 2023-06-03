using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2_Entity
{
    public partial class Form1 : Form
    {
        PhotoStudioEntities db = new PhotoStudioEntities(); // подключение к контексту БД
        public Form1()
        {
            InitializeComponent();
            refreshTableClient();
            refreshCbClient();
            refreshTableService();
            refreshCbService();
            refreshTableOrder();
        }

        private void button1_Click(object sender, EventArgs e) // кнопка добавить клиента
        {
            Client client = new Client()
            {
                Name = textBox1.Text,
                Phone = textBox2.Text,
                Note = textBox3.Text
            };
            db.Clients.Add(client);
            db.SaveChanges();
            refreshTableClient();
            refreshCbClient();
        }
        void refreshTableClient() // обновление таблицы клиенты
        {
            tableClient.DataSource = db.Clients.ToList();
            tableClient.Refresh();
            tableClient.Columns["Orders"].Visible = false; // скрываем столбец "заказы" на вкладке клиенты
        }

        private void button2_Click(object sender, EventArgs e) // кнопка удалить клиента
        {
            int index = tableClient.CurrentRow.Index;
            var client = db.Clients.Find(Convert.ToInt32(tableClient[0, index].Value));
            db.Clients.Remove(client);
            db.SaveChanges();
            refreshTableClient();
            refreshCbClient();

        }

        private void button3_Click(object sender, EventArgs e) // кнопка добавить услугу
        {
            Service service = new Service()
            {
                Name = textBox4.Text,
                Note = textBox5.Text
            };
            db.Services.Add(service);
            db.SaveChanges();
            refreshTableService();
            refreshCbService();
        }
        void refreshTableService() // обновление таблицы услуги
        {
            tableService.DataSource = db.Services.ToList();
            tableService.Refresh();
            tableService.Columns["Orders"].Visible = false; // скрываем столбец "заказы" на вкладке услуги
        }

        private void button4_Click(object sender, EventArgs e) // кнопка удалить услугу
        {
            int index = tableService.CurrentRow.Index;
            var service = db.Services.Find(Convert.ToInt32(tableService[0, index].Value));
            db.Services.Remove(service);
            db.SaveChanges();
            refreshTableService();
            refreshCbService();
        }

        private void button5_Click(object sender, EventArgs e) // кнопка добавить заказ
        {
            Order order = new Order()
            {
                Client_id = Convert.ToInt32(cbClient.SelectedValue),
                Service_id = Convert.ToInt32(cbService.SelectedValue),
                Price = Convert.ToInt32(textBox6.Text),
                Note = textBox7.Text
            };
            db.Orders.Add(order);
            db.SaveChanges();
            refreshTableOrder();
        }
        
        void refreshTableOrder() // обновление таблицы заказы
        {
            // Создание запроса к таблице заказы для получения определенных полей и их вывод на вкладку "Заказ"
            var query = from order in db.Orders select new { order.Id, Client_name = order.Client.Name, Service_name = order.Service.Name, order.Price, order.Note };
            tableOrder.DataSource = query.ToList();
        }
        void refreshCbClient() // заполнение клиентов в выпадающий список (combobox)
        {
            var cl = from client in db.Clients select client;
            cbClient.DataSource = cl.ToList();
            cbClient.DisplayMember = "Name";
            cbClient.ValueMember = "Id";
        }
        void refreshCbService() // заполнение услуг в выпадающий список (combobox)
        {
            var sv = from service in db.Services select service;
            cbService.DataSource = sv.ToList();
            cbService.DisplayMember = "Name";
            cbService.ValueMember = "Id";
        }

        private void button6_Click(object sender, EventArgs e) // кнопка удалить заказ
        {
            int index = tableOrder.CurrentRow.Index;
            var order = db.Orders.Find(Convert.ToInt32(tableOrder[0, index].Value));
            db.Orders.Remove(order);
            db.SaveChanges();
            refreshTableOrder();
        }
    }
}
