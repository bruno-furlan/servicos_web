using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Diagnostics;

namespace FipeDownloader
{
    public partial class Form1 : Form
    {
        #region variaveis global
        List<JDCMarcas> gMarcas = new List<JDCMarcas>();
        List<JDCModelos> gModelos = new List<JDCModelos>();
        List<JDCAnos> gAnos = new List<JDCAnos>();
        List<string> marcasAdd = new List<string>();
        List<string> modelosAdd = new List<string>();
        List<string[]> listCtrl = new List<string[]>();
        bool primeira = false, cancelar = false;
        String tipoVeiculo = "";
        int cont = 0;

        WebClient w = new WebClient();
        #endregion

        #region classes
        /// <summary>
        /// Classe das marcas
        /// </summary>
        public class JDCMarcas
        {
            public string name { get; set; }
            public string fipe_name { get; set; }
            public int order { get; set; }
            public string key { get; set; }
            public int id { get; set; }
        }

        /// <summary>
        /// Classe dos modelos
        /// </summary>
        public class JDCModelos
        {
            public string fipe_marca { get; set; }
            public string name { get; set; }
            public string marca { get; set; }
            public string key { get; set; }
            public string id { get; set; }
            public string fipe_name { get; set; }
        }

        /// <summary>
        /// Classe dos anos
        /// </summary>
        public class JDCAnos
        {
            public string fipe_marca { get; set; }
            public string fipe_codigo { get; set; }
            public string name { get; set; }
            public string marca { get; set; }
            public string key { get; set; }
            public string veiculo { get; set; }
            public string id { get; set; }
        }

        /// <summary>
        /// Classe dos detalhes
        /// </summary>
        public class JDCDetalhe
        {
            public string referencia { get; set; }
            public string fipe_codigo { get; set; }
            public string name { get; set; }
            public string combustivel { get; set; }
            public string marca { get; set; }
            public string ano_modelo { get; set; }
            public string preco { get; set; }
            public string key { get; set; }
            public double time { get; set; }
            public string veiculo { get; set; }
            public string id { get; set; }
        }

        /// <summary>
        /// Classe para criar itens do combo com um valor customizado
        /// </summary>
        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
        #endregion classes

        #region metodos
        /// <summary>
        /// Metodo para download da FIPE
        /// </summary>
        private void DownloadFIPE()
        {
            string idMarca, idModelo, txtMarca, txtModelo, idTipo, nomeArq, 
                   pathTemp = System.IO.Path.GetTempPath(), datainicio = System.DateTime.Now.ToString();
            bool achou;
            System.IO.StreamWriter file;
            List<string> lFinal = new List<string>();

            idMarca = cmbMarcas.SelectedIndex > 0 ? ((ComboboxItem)(cmbMarcas.SelectedItem)).Value.ToString() : "";
            txtMarca = cmbMarcas.SelectedIndex > 0 ? ((ComboboxItem)(cmbMarcas.SelectedItem)).Text : "";
            idModelo = cmbModelos.SelectedIndex > 0 ? ((ComboboxItem)(cmbModelos.SelectedItem)).Value.ToString() : "";
            txtModelo = cmbModelos.SelectedIndex > 0 ? ((ComboboxItem)(cmbModelos.SelectedItem)).Text : "";
            idTipo = ((ComboboxItem)(cmbTipos.SelectedItem)).Value.ToString();

            nomeArq = pathTemp + txtMarca + "_" + idTipo + ".csv";
            
            //Se os combo Marca e modelo estiverem preenchidos
            if (cmbMarcas.SelectedIndex > 0 && cmbModelos.SelectedIndex > 0)
            {
                file = new System.IO.StreamWriter(nomeArq, false, Encoding.Default);
                escreveCabecalho(file);

                messageStatus("Salvando " + txtMarca + " / " + txtModelo + " . . .");
                achou = procurarModelos(txtModelo);
                if (achou)
                {
                    foreach (JDCAnos m in gAnos)
                    {
                        if (String.Compare(m.veiculo.ToUpper(), txtModelo) == 0)
                        {
                            escreveLinha(file, idMarca, idModelo, m.id, txtMarca, txtModelo, m.name);
                            freeForm();
                            if (cancelar)
                                break;
                        }
                    }
                }
                else
                {
                    JDCAnos[] jAnos = adicionaAnos(idMarca, idModelo, txtModelo);

                    foreach (JDCAnos m in jAnos)
                    {
                        escreveLinha(file, idMarca, idModelo, m.id, txtMarca, txtModelo, m.name);
                        freeForm();
                        if (cancelar)
                            break;
                    }
                }
                file.Close();
            }
            //Se só combo Marca estiver preenchido
            else if (cmbMarcas.SelectedIndex > 0)
            {
                file = new System.IO.StreamWriter(nomeArq, false, Encoding.Default);
                escreveCabecalho(file);

                foreach (ComboboxItem iModelo in cmbModelos.Items)
                {
                    if (iModelo.Text == "TODOS")
                        continue;

                    txtModelo = iModelo.Text;
                    idModelo = iModelo.Value.ToString();

                    messageStatus("Salvando " + txtMarca + " / " + txtModelo + " . . .");
                    achou = procurarModelos(txtModelo);
                    if (achou)
                    {
                        foreach (JDCAnos m in gAnos)
                        {
                            if (String.Compare(m.veiculo.ToUpper(), txtModelo) == 0)
                            {
                                escreveLinha(file, idMarca, idModelo, m.id, txtMarca, txtModelo, m.name);
                                freeForm();
                                if (cancelar)
                                    break;
                            }
                        }
                    }
                    else
                    {
                        JDCAnos[] jAnos = adicionaAnos(idMarca, idModelo, txtModelo);
                        foreach (JDCAnos m in jAnos)
                        {
                            escreveLinha(file, idMarca, idModelo, m.id, txtMarca, txtModelo, m.name);
                            freeForm();
                            if (cancelar)
                                break;
                        }
                    }
                    if (cancelar)
                        break;
                }
                file.Close();
            }
            //Se nenhum dos combos estiverem preenchidos
            else
            {
                foreach (JDCMarcas iMarcas in gMarcas)
                {
                    List<JDCModelos> lModelos = new List<JDCModelos>();

                    txtMarca = iMarcas.name.ToUpper();
                    idModelo = iMarcas.id.ToString();
                    idMarca = iMarcas.id.ToString();

                    nomeArq = pathTemp + txtMarca + "_" + idTipo + ".csv";

                    file = new System.IO.StreamWriter(nomeArq, false, Encoding.Default);
                    escreveCabecalho(file);

                    achou = procurarMarcas(txtMarca);
                    if (!achou)
                    {
                        JDCModelos[] jModelos = adicionaModelos(idModelo, txtMarca);
                        lModelos.AddRange(jModelos);
                    }
                    else
                    {
                        foreach (JDCModelos item in gModelos)
                        {
                            if (string.Compare(item.marca.ToUpper(),txtMarca) == 0)
                                lModelos.Add(item);
                        }
                    }

                    foreach (JDCModelos m in lModelos)
                    {
                        idModelo = m.id;
                        txtModelo = m.name.ToUpper();
                        messageStatus("Salvando " + txtMarca + " / " + txtModelo + " . . .");
                        achou = procurarModelos(txtModelo);
                        if (achou)
                        {
                            foreach (JDCAnos a in gAnos)
                            {
                                if (String.Compare(a.veiculo.ToUpper(), txtModelo) == 0)
                                {
                                    escreveLinha(file, idMarca, idModelo, a.id, txtMarca, txtModelo, a.name);
                                    freeForm();
                                    if (cancelar)
                                        break;
                                }
                            }
                        }
                        else
                        {
                            JDCAnos[] jAnos = adicionaAnos(idMarca, idModelo, txtModelo);
                            foreach (JDCAnos a in jAnos)
                            {
                                escreveLinha(file, idMarca, idModelo, a.id, txtMarca, txtModelo, a.name);
                                freeForm();
                                if (cancelar)
                                    break;
                            }
                        }
                        if (cancelar)
                            break;
                    }
                    file.Close();
                    if (cancelar)
                        break;
                }
            }
            messageStatus("");
            if (!cancelar)
            {
                MessageBox.Show("Arquivos gerados em: " + pathTemp + "\n\nExecução realizada entre: " + datainicio + " e " + System.DateTime.Now.ToString(), "Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start("explorer.exe", @"/select, " + nomeArq);
            }
        }

        /// <summary>
        /// Metodo para escrever o cabeçalho no arquivo
        /// </summary>
        /// <param name="file">Arquivo</param>
        private void escreveCabecalho(StreamWriter file)
        {
            if (chkIncluirDet.Checked)
                file.WriteLine(("marca;modelo;ano;mes ref;cod fipe;preco").ToUpper());
            else
                file.WriteLine(("marca;modelo;ano").ToUpper());
        }

        /// <summary>
        /// Metodo para escriver linha no arquivo
        /// </summary>
        /// <param name="file">Arquivo</param>
        /// <param name="idMarca">identificador da marca</param>
        /// <param name="idModelo">identificador do modelo</param>
        /// <param name="idAno">identificador do ano</param>
        /// <param name="txtMarca">Marca</param>
        /// <param name="txtModelo">Modelo</param>
        /// <param name="txtAno">Ano</param>
        private void escreveLinha(StreamWriter file, string idMarca, string idModelo, string idAno,
                                  string txtMarca, string txtModelo, string txtAno)
        {
            if (chkIncluirDet.Checked)
            {
                JDCDetalhe d = adicionaDetalhes(idMarca, idModelo, idAno);
                file.WriteLine(fnTroca(txtMarca + ";" + txtModelo + ";" + txtAno + ";" +
                               d.referencia + ";" + d.fipe_codigo + ";" + d.preco).ToUpper());
            }
            else
                file.WriteLine(fnTroca(txtMarca + ";" + txtModelo + ";" + txtAno).ToUpper());
        }

        /// <summary>
        /// Metodo para mostrar mensagem no statusbar
        /// </summary>
        /// <param name="msg">Mensagem</param>
        private void messageStatus(string msg)
        {
            staStatusBar.Text = msg;
        }

        /// <summary>
        /// Metodo para dar um refresh no form
        /// </summary>
        private void freeForm()
        {
            cont++;
            if (cont % 10 == 0)
            {
                Application.DoEvents();
                cont = 0;
            }
        }

        /// <summary>
        /// Metodo para carregar as marcas
        /// </summary>
        private void carregaMarcas()
        {
            String idTipo;
            idTipo = ((ComboboxItem)(cmbTipos.SelectedItem)).Value.ToString();

            switch (idTipo)
            {
                case "1":
                    tipoVeiculo = "carros";
                    break;
                case "2":
                    tipoVeiculo = "caminhoes";
                    break;
                case "3":
                    tipoVeiculo = "motos";
                    break;
            }

            gMarcas = new List<JDCMarcas>();
            gModelos = new List<JDCModelos>();
            gAnos = new List<JDCAnos>();
            marcasAdd = new List<string>();
            modelosAdd = new List<string>();

            cmbMarcas.Items.Clear();
            cmbModelos.Items.Clear();
            cmbAnos.Items.Clear();

            ComboboxItem i = new ComboboxItem();
            i.Text = "TODAS";
            i.Value = "";
            cmbMarcas.Items.Add(i);

            i = new ComboboxItem();
            i.Text = "TODOS";
            i.Value = "";
            cmbModelos.Items.Add(i);
            cmbAnos.Items.Add(i);

            cmbMarcas.SelectedIndex = 0;
            cmbModelos.SelectedIndex = 0;
            cmbAnos.SelectedIndex = 0;

            JDCMarcas[] jMarcas = adicionaMarcas();
            adicionaMarcasCombo();
        }

        /// <summary>
        /// Metodo para adicionar as marcas ao array global
        /// </summary>
        /// <returns></returns>
        private JDCMarcas[] adicionaMarcas()
        {
            //string marcas = w.DownloadString("http://fipeapi.appspot.com/api/1/carros/marcas.json");
            //http://jsonutils.com/

            w.Encoding = System.Text.Encoding.UTF8;
            try
            {
                Stream marcas = w.OpenRead("http://fipeapi.appspot.com/api/1/" + tipoVeiculo + "/marcas.json");
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(JDCMarcas[]));
                JDCMarcas[] jMarcas = (JDCMarcas[])serializer.ReadObject(marcas);
                gMarcas.AddRange(jMarcas);

                return jMarcas;
            }
            catch (WebException)
            {
                MessageBox.Show("Não foi possivel carregar as marcas . . .","Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show("Ocorreu um erro: " + e, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        /// <summary>
        /// Metodo para adicionar os modelos ao array global
        /// </summary>
        /// <param name="idModelo">identificador do modelo da FIPE</param>
        /// <param name="txtMarca">Marca para adicionar no controle global</param>
        /// <returns></returns>
        private JDCModelos[] adicionaModelos(string idModelo, string txtMarca)
        {
            try
            {
                Stream modelos = w.OpenRead("http://fipeapi.appspot.com/api/1/" + tipoVeiculo + "/veiculos/" + idModelo + ".json");
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(JDCModelos[]));
                JDCModelos[] jModelos = (JDCModelos[])serializer.ReadObject(modelos);
                foreach (JDCModelos item in jModelos)
                {
                    if (item.marca == null)
                        item.marca = txtMarca;
                }
                gModelos.AddRange(jModelos);
                marcasAdd.Add(txtMarca.ToUpper());

                return jModelos;
            }
            catch (WebException)
            {
                MessageBox.Show("Não foi possivel carregar os modelos . . .", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show("Ocorreu um erro: " + e, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        /// <summary>
        /// Metodo para adicionar os anos ao array global
        /// </summary>
        /// <param name="idMarca">identificador da marca da FIPE</param>
        /// <param name="idModelo">identificador do modelo da FIPE</param>
        /// <param name="txtMarca">Modelo para adicionar no controle global</param>
        /// <returns></returns>
        private JDCAnos[] adicionaAnos(string idMarca, string idModelo, string txtModelo)
        {
            try
            {
                Stream anos = w.OpenRead("http://fipeapi.appspot.com/api/1/" + tipoVeiculo + "/veiculo/" + idMarca + "/" + idModelo + ".json");
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(JDCAnos[]));
                JDCAnos[] jAnos = (JDCAnos[])serializer.ReadObject(anos);
                foreach (JDCAnos item in jAnos)
                {
                    if (item.veiculo == null)
                        item.veiculo = txtModelo;
                }
                gAnos.AddRange(jAnos);
                modelosAdd.Add(txtModelo.ToUpper());

                return jAnos;
            }
            catch (WebException)
            {
                MessageBox.Show("Não foi possivel carregar os anos . . .", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show("Ocorreu um erro: " + e, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        /// <summary>
        /// Metodo para pesquisar os detalhes
        /// </summary>
        /// <param name="idMarca">identificador da marca da FIPE</param>
        /// <param name="idModelo">identificador do modelo da FIPE</param>
        /// <param name="idAno">identificador do ano da FIPE</param>
        /// <returns></returns>
        private JDCDetalhe adicionaDetalhes(string idMarca, string idModelo, string idAno)
        {
            try
            {
                Stream detalhes = w.OpenRead("http://fipeapi.appspot.com/api/1/" + tipoVeiculo + "/veiculo/" + idMarca + "/" + idModelo + "/" + idAno + ".json");
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(JDCDetalhe));
                JDCDetalhe jDetalhes = (JDCDetalhe)serializer.ReadObject(detalhes);

                return jDetalhes;
            }
            catch (WebException)
            {
                MessageBox.Show("Não foi possivel carregar os detalhes . . .", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show("Ocorreu um erro: " + e, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        /// <summary>
        /// Metodo para corrigir a string de saida do arquivo
        /// </summary>
        /// <param name="ori">string que sera trocada</param>
        /// <returns></returns>
        private string fnTroca(string ori)
        {
            string ret = ori;

            // Replace accented characters
            ret = ret.Replace("\\u00c0", "À");
            ret = ret.Replace("\\u00c1", "Á");
            ret = ret.Replace("\\u00c2", "Â");
            ret = ret.Replace("\\u00c3", "Ã");
            ret = ret.Replace("\\u00c4", "Ä");
            ret = ret.Replace("\\u00c5", "Å");
            ret = ret.Replace("\\u00c6", "Æ");
            ret = ret.Replace("\\u00c7", "Ç");
            ret = ret.Replace("\\u00c8", "È");
            ret = ret.Replace("\\u00c9", "É");
            ret = ret.Replace("\\u00ca", "Ê");
            ret = ret.Replace("\\u00cb", "Ë");
            ret = ret.Replace("\\u00cc", "Ì");
            ret = ret.Replace("\\u00cd", "Í");
            ret = ret.Replace("\\u00ce", "Î");
            ret = ret.Replace("\\u00cf", "Ï");
            ret = ret.Replace("\\u00d1", "Ñ");
            ret = ret.Replace("\\u00d2", "Ò");
            ret = ret.Replace("\\u00d3", "Ó");
            ret = ret.Replace("\\u00d4", "Ô");
            ret = ret.Replace("\\u00d5", "Õ");
            ret = ret.Replace("\\u00d6", "Ö");
            ret = ret.Replace("\\u00d8", "Ø");
            ret = ret.Replace("\\u00d9", "Ù");
            ret = ret.Replace("\\u00da", "Ú");
            ret = ret.Replace("\\u00db", "Û");
            ret = ret.Replace("\\u00dc", "Ü");
            ret = ret.Replace("\\u00dd", "Ý");

            // Now lower case accents
            ret = ret.Replace("\\u00df", "ß");
            ret = ret.Replace("\\u00e0", "à");
            ret = ret.Replace("\\u00e1", "á");
            ret = ret.Replace("\\u00e2", "â");
            ret = ret.Replace("\\u00e3", "ã");
            ret = ret.Replace("\\u00e4", "ä");
            ret = ret.Replace("\\u00e5", "å");
            ret = ret.Replace("\\u00e6", "æ");
            ret = ret.Replace("\\u00e7", "ç");
            ret = ret.Replace("\\u00e8", "è");
            ret = ret.Replace("\\u00e9", "é");
            ret = ret.Replace("\\u00ea", "ê");
            ret = ret.Replace("\\u00eb", "ë");
            ret = ret.Replace("\\u00ec", "ì");
            ret = ret.Replace("\\u00ed", "í");
            ret = ret.Replace("\\u00ee", "î");
            ret = ret.Replace("\\u00ef", "ï");
            ret = ret.Replace("\\u00f0", "ð");
            ret = ret.Replace("\\u00f1", "ñ");
            ret = ret.Replace("\\u00f2", "ò");
            ret = ret.Replace("\\u00f3", "ó");
            ret = ret.Replace("\\u00f4", "ô");
            ret = ret.Replace("\\u00f5", "õ");
            ret = ret.Replace("\\u00f6", "ö");
            ret = ret.Replace("\\u00f8", "ø");
            ret = ret.Replace("\\u00f9", "ù");
            ret = ret.Replace("\\u00fa", "ú");
            ret = ret.Replace("\\u00fb", "û");
            ret = ret.Replace("\\u00fc", "ü");
            ret = ret.Replace("\\u00fd", "ý");
            ret = ret.Replace("\\u00ff", "ÿ");

            return ret;
        }

        /// <summary>
        /// Metodo para adicionar as marcas ao combo
        /// </summary>
        /// <param name="txtModelo">modelo especifico que vai ser adicionado</param>
        /// <param name="compara">parametro opcional, true adiciona um especifico, false adiciona todos</param>
        private void adicionaAnosCombo(string txtModelo, bool compara = true)
        { 
            ComboboxItem item = new ComboboxItem();
            item.Text = "TODOS";
            item.Value = "";
            cmbAnos.Items.Add(item);
            foreach (JDCAnos m in gAnos)
            {
                if (String.Compare(m.veiculo.ToUpper(), txtModelo) == 0 || !compara)
                {
                    //cmbAnos.Items.Add(m.name.ToUpper() + " - " + m.id);
                    item = new ComboboxItem();
                    item.Text = m.name.ToUpper();
                    item.Value = m.id;
                    cmbAnos.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// Metodo para adicionar as marcas ao combo
        /// </summary>
        private void adicionaMarcasCombo()
        {
            ComboboxItem item;
            foreach (JDCMarcas m in gMarcas)
            {
                //cmbMarcas.Items.Add(m.name.ToUpper() + " - " + m.id);
                item = new ComboboxItem();
                item.Text = m.name.ToUpper();
                item.Value = m.id;
                cmbMarcas.Items.Add(item);
            }
        }

        /// <summary>
        /// Metodo para adicionar os modelos ao combo
        /// </summary>
        /// <param name="txtMarca">marca especifica que vai ser adicionada</param>
        /// <param name="compara">parametro opcional, true adiciona uma especifica, false adiciona todas</param>
        private void adicionaModelosCombo(string txtMarca, bool compara = true)
        {
            ComboboxItem item = new ComboboxItem();
            cmbModelos.Items.Clear();
            item.Text = "TODOS";
            item.Value = "";
            cmbModelos.Items.Add(item);

            foreach (JDCModelos m in gModelos)
            {
                if (string.Compare(m.marca.ToUpper(), txtMarca) == 0 || !compara)
                {
                    //cmbModelos.Items.Add(m.name.ToUpper() + " - " + m.id);
                    item = new ComboboxItem();
                    item.Text = m.name.ToUpper();
                    item.Value = m.id;
                    cmbModelos.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// Metodo identificar se uma marca ja foi feito o download
        /// </summary>
        /// <param name="txtMarca">marca a ser procurada</param>
        /// <returns>true se achou e false se não achou</returns>
        private bool procurarMarcas(string txtMarca)
        {
            foreach (string i in marcasAdd)
            {
                if (String.Compare(i, txtMarca) == 0)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Metodo identificar se um modelo ja foi feito o download
        /// </summary>
        /// <param name="txtModelo">modelo a ser procurado</param>
        /// <returns>true se achou e false se não achou</returns>
        private bool procurarModelos(string txtModelo)
        {
            foreach (string i in modelosAdd)
            {
                if (String.Compare(i, txtModelo) == 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Metodo para adicionar os detalhes a tela
        /// </summary>
        /// <param name="txtAno"></param>
        private void adicionaDetalhesTela(JDCDetalhe jDetalhes)
        {
            txtMesRef.Text = jDetalhes.referencia;
            txtCodFipe.Text = jDetalhes.fipe_codigo;
            txtPreco.Text = jDetalhes.preco;
        }

        /// <summary>
        /// Metodo para limpar os detalhes a tela
        /// </summary>
        private void limpaTela()
        {
            JDCDetalhe jDetalhes = new JDCDetalhe();
            jDetalhes.referencia = "";
            jDetalhes.fipe_codigo = "";
            jDetalhes.preco = "";
            adicionaDetalhesTela(jDetalhes);
        }

        /// <summary>
        /// Metodo para habilitar controles
        /// </summary>
        private void HabilitaCtrl()
        {
            foreach (Control x in this.Controls)
            {
                string t = x.GetType().ToString();
                if (t.IndexOf("ComboBox") >= 0 || t.IndexOf("CheckBox") >= 0)
                {
                    foreach (string[] item in listCtrl)
                    {
                        if (string.Compare(item[0], x.Name) == 0)
                        {
                            x.Enabled = Convert.ToBoolean(item[1]);
                            break;
                        }
                    }
                }
            }
            listCtrl.Clear();
        }

        /// <summary>
        /// Metodo para desabilitar controles
        /// </summary>
        private void DesabilitaCtrl()
        {
            foreach (Control x in this.Controls)
            {
                string t = x.GetType().ToString();
                if (t.IndexOf("ComboBox") >= 0 || t.IndexOf("CheckBox") >= 0)
                {
                    listCtrl.Add(new string[] { x.Name, x.Enabled.ToString() });
                    x.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Método para carregar tipos de veículos
        /// </summary>
        private void carregaTipos()
        {

            ComboboxItem item = new ComboboxItem();
            item.Text = "CARROS E UTILITÁRIOS";
            item.Value = "1";
            cmbTipos.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "CAMINHÕES E MICRO-ÔNIBUS";
            item.Value = "2";
            cmbTipos.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "MOTOS";
            item.Value = "3";
            cmbTipos.Items.Add(item);

            cmbTipos.SelectedIndex = 0;
        }
        #endregion metodos


        public Form1()
        {
            InitializeComponent();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (string.Compare(btnDownload.Text,"Download") == 0)
            {
                cancelar = false;
                DesabilitaCtrl();
                btnDownload.Text = "Cancelar";
                DownloadFIPE();
                btnDownload.Text = "Download";
                HabilitaCtrl();
            }
            else
            {
                cancelar = true;
                btnDownload.Text = "Download";
                HabilitaCtrl();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            staStatusBar.Text = "Carregando marcas . . .";
            carregaTipos();
            carregaMarcas();
            staStatusBar.Text = "";
        }

        private void cmbMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idModelo, txtMarca;
            bool achou = false;

            if (!primeira)
                return;

            if (cmbMarcas.SelectedIndex >= 0)
            {
                cmbModelos.Enabled = false;
                cmbModelos.SelectedIndex = 0;
                cmbAnos.Enabled = false;
                cmbAnos.SelectedIndex = 0;
                if (cmbMarcas.SelectedIndex > 0)
                {
                    cmbModelos.Enabled = true;
                    idModelo = ((ComboboxItem)(cmbMarcas.SelectedItem)).Value.ToString();
                    txtMarca = ((ComboboxItem)(cmbMarcas.SelectedItem)).Text;

                    achou = procurarMarcas(txtMarca);

                    if (achou)
                    {
                        adicionaModelosCombo(txtMarca);
                    }
                    else
                    {
                        JDCModelos[] jModelos = adicionaModelos(idModelo, txtMarca);
                        adicionaModelosCombo(txtMarca);
                    }
                }
            }
        }

        private void cmbModelos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idMarca, idModelo, txtModelo;
            bool achou = false;

            if (!primeira)
                return;

            cmbAnos.Enabled = false;
            cmbAnos.SelectedIndex = 0;
            limpaTela();
            if (cmbModelos.SelectedIndex > 0)
            {
                idMarca = ((ComboboxItem)(cmbMarcas.SelectedItem)).Value.ToString();
                cmbAnos.Enabled = true;
                cmbAnos.Items.Clear();
                idModelo = ((ComboboxItem)(cmbModelos.SelectedItem)).Value.ToString();
                txtModelo = ((ComboboxItem)(cmbModelos.SelectedItem)).Text;

                achou = procurarModelos(txtModelo);
                if (achou)
                {
                    adicionaAnosCombo(txtModelo);
                }
                else
                {
                    JDCAnos[] jAnos = adicionaAnos(idMarca, idModelo, txtModelo);
                    adicionaAnosCombo(txtModelo);
                }
            }
        }

        private void cmbAnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idMarca, idModelo, idAno, txtAno;

            if (!primeira)
            {
                primeira = true;
                return;
            }

            if (cmbAnos.SelectedIndex > 0)
            {
                idMarca = ((ComboboxItem)(cmbMarcas.SelectedItem)).Value.ToString();
                idModelo = ((ComboboxItem)(cmbModelos.SelectedItem)).Value.ToString();
                idAno = ((ComboboxItem)(cmbAnos.SelectedItem)).Value.ToString();
                txtAno = ((ComboboxItem)(cmbAnos.SelectedItem)).Text;

                JDCDetalhe jDetalhes = adicionaDetalhes(idMarca, idModelo, idAno);
                adicionaDetalhesTela(jDetalhes);
            }
            else
                limpaTela();
        }

        private void cmbTipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            primeira = true;
            carregaMarcas();
        }
    }
}
