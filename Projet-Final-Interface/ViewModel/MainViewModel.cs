using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Projet_Final_Interface.Command;
using Projet_Final_Interface.Model;
using Projet_Final_Interface.View;

namespace Projet_Final_Interface.ViewModel
{
    class MainViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private Joueur theJoueur;
        private Personnage thePersonnage;
        public ObservableCollection<Button> listePersonnage;
        private List<string> listeClasse= new List<string>();
        private List<string> listeDivinite = new List<string>();
        private List<string> listeAlignement = new List<string>();
        private List<string> listeRace = new List<string>();
        private List<string> listeArme = new List<string>();
        private List<string> listeArmure = new List<string>();
        private string exception;
        private Random randomiseur;
        Style style;
        private int personnageId;

        public MainViewModel() : base()
        {
            listeRace = new List<string>();
            listeAlignement = new List<string>();
            listeDivinite = new List<string>();
            listeClasse = new List<string>();
            listePersonnage = new ObservableCollection<Button>();
            style = new Style(typeof(Button));
            Setter s = new Setter();
            s.Property = Button.WidthProperty;
            s.Value = 100;
            style.Setters.Add(s);
            //style.Setters.Add(new Setter(Control.HeightProperty, 150));
            //style.Setters.Add(new Setter(Control.BorderThicknessProperty, 2));
            theJoueur = new Joueur();
            thePersonnage = new Personnage();
            exception = "";
            ConnexionCommand = new BaseCommand(Connexion, obj => true);
            InscriptionCommand = new BaseCommand(Inscription, obj => true);
            openCreationCommand = new BaseCommand(openCreation, obj => true);
            openConsultationCommand = new BaseCommand(openConsultation, obj => true);
            OpenInscCommand = new BaseCommand(openInscription, obj => true);
            RandomForceCommand = new BaseCommand(RandomForce, obj => true);
            RandomDexteriteCommand = new BaseCommand(RandomDexterite, obj => true);
            RandomConstitutionCommand = new BaseCommand(RandomConstitution, obj => true);
            RandomIntelligenceCommand = new BaseCommand(RandomIntelligence, obj => true);
            RandomSagesseCommand = new BaseCommand(RandomSagesse, obj => true);
            RandomCharismeCommand = new BaseCommand(RandomCharisme, obj => true);
            randomiseur = new Random();
        }

        public Personnage ThePersonnage { get => thePersonnage; set { thePersonnage = value; OnPropertyChanged("ThePersonnage"); } }

        public int PersoId { get => personnageId; set { personnageId = value; OnPropertyChanged("PersonnageId"); } }


        public Joueur TheJoueur { get => theJoueur; set { theJoueur = value; OnPropertyChanged("TheJoueur"); } }

        public ObservableCollection<Button> ListePersonnage { get => listePersonnage; set { listePersonnage = value; OnPropertyChanged("ListePersonnage"); } }

        public List<string> ListeClasse { get => listeClasse; set { listeClasse = value; OnPropertyChanged("ListeClasse"); } }

        public List<string> ListeDivinite { get => listeDivinite; set { listeDivinite = value; OnPropertyChanged("ListeDivinite"); } }

        public List<string> ListeRace { get => listeRace; set { listeRace = value; OnPropertyChanged("ListeRace"); } }

        public List<string> ListeAlignement { get => listeAlignement; set { listeAlignement = value; OnPropertyChanged("ListeAlignement"); } }

        public List<string> ListeArme { get => listeAlignement; set { listeAlignement = value; OnPropertyChanged("ListeArme"); } }

        public List<string> ListeArmure { get => listeAlignement; set { listeAlignement = value; OnPropertyChanged("ListeArmure"); } }
        public ICommand RandomForceCommand { get; set; }

        public ICommand ConnexionCommand { get; set; }
        public ICommand openCreationCommand { get; set; }

        public ICommand openConsultationCommand { get; set; }

        public ICommand InscriptionCommand { get; set; }

        public ICommand OpenInscCommand { get; set; }


        public ICommand RandomDexteriteCommand { get; set; }
        public ICommand RandomConstitutionCommand { get; set; }
        public ICommand RandomIntelligenceCommand { get; set; }
        public ICommand RandomSagesseCommand { get; set; }
        public ICommand RandomCharismeCommand { get; set; }
        public string Exception { get => exception; set { exception = value; } }


        public void OpenGestionPerso()
        {
            loadPersonnage();
            GestionPerso fenetre = new GestionPerso();
            fenetre.Show();
            fenetre.DataContext = this;
         

        }

        private void Connexion(object obj)
        {
            string testPassword = "";
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP(1) id,nom,prenom,gamerTag,motDePasse,sel FROM tblJoueur WHERE gamerTag=@param1 AND motDePasse=HASHBYTES('SHA2_512',@mdp+CAST(sel AS NVARCHAR(36)))", con);
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = TheJoueur.Courriel;
                cmd.Parameters.Add("@mdp", SqlDbType.VarChar, 255).Value = TheJoueur.Password;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "tblJoueur");

                if (dataSet.Tables[0].Rows !=null)
                {
                    theJoueur.Courriel = dataSet.Tables[0].Rows[0].ItemArray[3].ToString();
                    theJoueur.Id = Convert.ToInt32(dataSet.Tables[0].Rows[0].ItemArray[0]);
                    OpenGestionPerso();
                }

            }
            catch (Exception e)
            {
                Exception = e.Message;
            }

        }

        private void Inscription(object obj)
        {
            try
            {
                //regarder si pas deja gamer tag
                SqlCommand cmd = new SqlCommand(" EXEC usp_insertionJoueurs @pNom=@nom,@pPrenom=@prenom,@pNomUtilisateur=@nomUtilisateur,@pMdp=@mdp;", con);
                cmd.Parameters.Add("@nom", SqlDbType.VarChar, 30).Value = theJoueur.Nom;
                cmd.Parameters.Add("@prenom", SqlDbType.VarChar, 30).Value = theJoueur.Prenom;
                cmd.Parameters.Add("@nomUtilisateur", SqlDbType.VarChar, 30).Value = theJoueur.Courriel;
                cmd.Parameters.Add("@mdp", SqlDbType.VarChar, 30).Value = theJoueur.Password;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                OpenGestionPerso();
            }
            catch (Exception e)
            {
                Exception = e.Message;
            }

        }

        private void openInscription(object obj)
        {
            Inscription page = new Inscription();
            page.Show();
            page.DataContext = this;
        }

        private void openCreation(object obj)
        {
            loadlistPersos();
            Window1 page = new Window1();
            page.Show();
            page.DataContext = this;
        }

        private void openConsultation(object obj)
        {
            RecherchePerso();
            Window2 page = new Window2();
            page.Show();
            page.DataContext = this;
        }

        private void RandomForce(object Obj)
        {
            ThePersonnage.Force=randomiseur.Next(1, 19);
            OnPropertyChanged(nameof(ThePersonnage));
         }

        private void RandomDexterite(object Obj)
        {
            ThePersonnage.Dexterite = randomiseur.Next(1, 19);
            OnPropertyChanged(nameof(ThePersonnage));
        }
        private void RandomConstitution(object Obj)
        {
            ThePersonnage.Constitution = randomiseur.Next(1, 19);
            OnPropertyChanged(nameof(ThePersonnage));
        }

        private void RandomIntelligence(object Obj)
        {
            ThePersonnage.Intelligence = randomiseur.Next(1, 19);
            OnPropertyChanged(nameof(ThePersonnage));
        }

        private void RandomSagesse(object Obj)
        {
            ThePersonnage.Sagesse = randomiseur.Next(1, 19);
            OnPropertyChanged(nameof(ThePersonnage));
        }

        private void RandomCharisme(object Obj)
        {
            ThePersonnage.Charisme = randomiseur.Next(1, 19);
            OnPropertyChanged(nameof(ThePersonnage));
        }

        private void loadPersonnage()
        {
            int cpt = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP(100) id,nom,race,classe,niveau FROM tblPersonnage WHERE joueur=@param1", con);
                cmd.Parameters.Add("@param1", SqlDbType.Int).Value = TheJoueur.Id;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "tblPersonnage");

                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    string p ="id : "+ row[0].ToString();
                    p+= "   nom : "+ row[1].ToString();
                    p += "   race : " + row[2].ToString();
                    p += "   classe : " + row[3].ToString();
                    p += "   niveau : " + row[4].ToString();
                   
                    listePersonnage.Add(new Button() { Content=p, Foreground = Brushes.Black , Visibility = Visibility.Visible , Background = Brushes.Orange, Width = 450, Height = 50,Margin= new Thickness(0, 30, 5, 0) });
                }

            }
            catch (Exception e)
            {
                Exception = e.Message;
            }


           

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void savePerso()
        {

        }

        private void RecherchePerso()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM vuePersonnage WHERE joueur=@param1 AND id=@param2", con);
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = TheJoueur.Id;
                cmd.Parameters.Add("@param2", SqlDbType.VarChar, 255).Value = personnageId;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "tblJoueur");

                if (dataSet.Tables[0].Rows != null)
                {
                    ThePersonnage.Nom = dataSet.Tables[0].Rows[0].ItemArray[1].ToString();
                    ThePersonnage.Divinite = dataSet.Tables[0].Rows[0].ItemArray[2].ToString();
                    thePersonnage.Alignement = dataSet.Tables[0].Rows[0].ItemArray[3].ToString();
                    thePersonnage.Classe = dataSet.Tables[0].Rows[0].ItemArray[4].ToString();
                    thePersonnage.BonusReflex = Convert.ToInt32(dataSet.Tables[0].Rows[0].ItemArray[5]);
                    thePersonnage.BonusVigueur = Convert.ToInt32(dataSet.Tables[0].Rows[0].ItemArray[6]);
                    thePersonnage.BonusVolonte = Convert.ToInt32(dataSet.Tables[0].Rows[0].ItemArray[7]);
                    thePersonnage.Niveau = Convert.ToInt32(dataSet.Tables[0].Rows[0].ItemArray[8]);
                    thePersonnage.Race = dataSet.Tables[0].Rows[0].ItemArray[9].ToString();
                    thePersonnage.Genre = dataSet.Tables[0].Rows[0].ItemArray[10].ToString();
                    thePersonnage.Age = Convert.ToInt32(dataSet.Tables[0].Rows[0].ItemArray[11]);
                    thePersonnage.Taille = Convert.ToInt32(dataSet.Tables[0].Rows[0].ItemArray[12]);
                    thePersonnage.CouleurYeux = dataSet.Tables[0].Rows[0].ItemArray[13].ToString();
                    thePersonnage.CouleurCheveux = dataSet.Tables[0].Rows[0].ItemArray[14].ToString();
                    OnPropertyChanged(nameof(ThePersonnage));
                }

            }
            catch (Exception e)
            {
                Exception = e.Message;
            }
        }

        private void loadlistPersos()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT nom FROM tblClasse", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "tblClasse");

                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    string p = row[0].ToString();

                    listeClasse.Add(p);
                }

                 cmd = new SqlCommand("SELECT nom,categorieTaille FROM tblRace", con);
                 adapter = new SqlDataAdapter(cmd);
                 dataSet = new DataSet();
                adapter.Fill(dataSet, "tblRace");

                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    string p = row[0].ToString()+ "   categorieTaille: "+ row[1].ToString();

                    listeRace.Add(p);
                }

                cmd = new SqlCommand("SELECT abreviation FROM tblAlignement", con);
                adapter = new SqlDataAdapter(cmd);
                dataSet = new DataSet();
                adapter.Fill(dataSet, "tblAlignement");

                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    string p = row[0].ToString();

                    listeAlignement.Add(p);
                }

                cmd = new SqlCommand("SELECT nom FROM tblDivinite", con);
                adapter = new SqlDataAdapter(cmd);
                dataSet = new DataSet();
                adapter.Fill(dataSet, "tblDivinite");

                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    string p = row[0].ToString();

                    listeDivinite.Add(p);
                }


                cmd = new SqlCommand("SELECT nom,categorieTaille FROM tblArme", con);
                adapter = new SqlDataAdapter(cmd);
                dataSet = new DataSet();
                adapter.Fill(dataSet, "tblArme");

                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    string p = row[0].ToString() + "   categorieTaille: " + row[1].ToString(); ;

                    listeDivinite.Add(p);
                }

                cmd = new SqlCommand("SELECT nom,categorieTaille FROM tblArmure", con);
                adapter = new SqlDataAdapter(cmd);
                dataSet = new DataSet();
                adapter.Fill(dataSet, "tblArmure");

                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    string p = row[0].ToString()+ "   categorieTaille: "+ row[1].ToString();

                    listeDivinite.Add(p);
                }

            }
            catch (Exception e)
            {
                Exception = e.Message;
            }
        }
    }
}
