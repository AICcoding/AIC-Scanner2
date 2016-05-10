using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIC_Scanner2
{
    public partial class Form1 : Form
    {
        String lokasi_simpan, nama_folder_penyimpanan_hasil_scanner;
        String[] daftar_file_direktori_aktif, list_nama_aktif;

        public Form1()
        {
            InitializeComponent();

            lokasi_simpan = "";
        }

        private void pilih_lokasi_simpan()
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();

            if(lokasi_simpan=="")
            {
                folderDlg.ShowNewFolderButton = true;
                // Show the FolderBrowserDialog.
                DialogResult result = folderDlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    lokasi_simpan = folderDlg.SelectedPath;

                    MessageBox.Show("Berhasil !\nLokasi simpan hasil pemindai pada direktori : " + lokasi_simpan, "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }


            else
            {
                if (MessageBox.Show("Lokasi simpan sudah di set pada direktori "+lokasi_simpan+"\nApakah anda ingin menggantinya ?", "Peringatan", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    folderDlg.ShowNewFolderButton = true;
                    // Show the FolderBrowserDialog.
                    DialogResult result = folderDlg.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        lokasi_simpan = folderDlg.SelectedPath;
                        MessageBox.Show("Berhasil !\nLokasi simpan hasil pemindai pada direktori : " + lokasi_simpan, "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            } 
        }

        private void setLokasiSimpanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pilih_lokasi_simpan();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(lokasi_simpan=="")
            {
                MessageBox.Show("Lokasi simpan belum ditentukan !.\nSilakan tentukan lokasi simpan pada :\nPengaturan -> Set lokasi simpan", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if(textBox2.Text.Trim()=="")
            {
                MessageBox.Show("Nama direktori untuk hasil pemindaian belum di tentukan !.\nSilakan ketik nama direktorinya terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (MessageBox.Show("Hasil pemindaian akan disimpan pada folder \"" + textBox2.Text.Trim() + "\".\nApakah anda yakin ? ", "Peringatan", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    nama_folder_penyimpanan_hasil_scanner = textBox2.Text.Trim();
                    textBox1.Text = lokasi_simpan + "\\" + nama_folder_penyimpanan_hasil_scanner;
                    if (!Directory.Exists(textBox1.Text))
                        Directory.CreateDirectory(textBox1.Text);
                    daftar_file_direktori_aktif = Directory.GetFiles(@"" + textBox1.Text);
                    tampilkan_isi();

                   
                    //scan...
                }
            }           
        }

        private void tampilkan_isi()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            buat_nama_file();

            for (int i = 0; i < daftar_file_direktori_aktif.Length; i++)
            {
                dataGridView1.Rows.Add(1);
                dataGridView1.Rows[i].Cells[0].Value = (i + 1).ToString();
                dataGridView1.Rows[i].Cells[1].Value = list_nama_aktif[i];
                dataGridView1.Rows[i].Cells[2].Value = daftar_file_direktori_aktif[i];
            }
        }

        private void buat_nama_file()
        {
            int start_indek = lokasi_simpan.Length + nama_folder_penyimpanan_hasil_scanner.Length + 2;
            list_nama_aktif = new string[daftar_file_direktori_aktif.Length];

            for (int i = 0; i < daftar_file_direktori_aktif.Length; i++)
            {
                list_nama_aktif[i] = daftar_file_direktori_aktif[i].Substring(start_indek);
            }
        }
    }
}
