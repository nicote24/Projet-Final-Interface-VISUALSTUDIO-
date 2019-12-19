using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Final_Interface.Model
{
    class Personnage
    {
        private string nom, couleurYeux, couleurCheveux, genre, langues, competences, classeArmure, race, classe, alignement, divinite, armure, arme;
        private int poidsKg, niveau, age, taille;
        private int force, dexterite, constitution, intelligence, sagesse, charisme, bonusReflex, bonusVigeur, bonusVolonte;

        public Personnage(string nom, string couleurYeux, string couleurCheveux, string genre, string langues, string competences, string classeArmure, string race, string classe, string alignement, string divinite, string armure, string arme, int poidsKg, int niveau, int age, int taille, int force, int dexterite, int constitution, int intelligence, int sagesse, int charisme, int bonusReflex, int bonusVigeur, int bonusVolonte)
        {
            this.nom = nom;
            this.couleurYeux = couleurYeux;
            this.couleurCheveux = couleurCheveux;
            this.genre = genre;
            this.langues = langues;
            this.competences = competences;
            this.classeArmure = classeArmure;
            this.race = race;
            this.classe = classe;
            this.alignement = alignement;
            this.divinite = divinite;
            this.armure = armure;
            this.arme = arme;
            this.poidsKg = poidsKg;
            this.niveau = niveau;
            this.age = age;
            this.taille = taille;
            this.force = force;
            this.dexterite = dexterite;
            this.constitution = constitution;
            this.intelligence = intelligence;
            this.sagesse = sagesse;
            this.charisme = charisme;
            this.bonusReflex = bonusReflex;
            this.bonusVigeur = bonusVigeur;
            this.bonusVolonte = bonusVolonte;
        }
        public Personnage()
        {
            nom = "";
            couleurCheveux = "";
            couleurYeux = "";
            genre = "";
            langues = "";
            competences = "";
            classeArmure = "";
            race = "";
            classe = "";
            alignement = "";
            divinite = "";
            armure = "";
            arme = "";
            poidsKg = 1;
            niveau = 0;
            age = 0;
            taille = 0;
            force = 0;
            dexterite = 0;
            constitution = 0;
            intelligence = 0;
            sagesse = 0;
            charisme = 0;
            bonusReflex = 0;
            bonusVigeur = 0;
            bonusVolonte = 0;
        }

        public string Nom { get => nom; set { nom = value; OnPropertyChanged("Nom"); } }

        public string CouleurYeux { get => couleurYeux; set { couleurYeux = value; OnPropertyChanged("CouleurYeux"); } }

        public string CouleurCheveux { get => couleurCheveux; set { couleurCheveux = value; OnPropertyChanged("CouleurCheveux"); } }

        public string Genre { get => genre; set { genre = value; OnPropertyChanged("Genre"); } }

        public string Langues { get => langues; set { langues = value; OnPropertyChanged("Langues"); } }

        public string Competences { get => competences; set { competences = value; OnPropertyChanged("Competences"); } }

        public string ClasseArmure { get => classeArmure; set { classeArmure = value; OnPropertyChanged("ClasseArmure"); } }

        public string Race { get => race; set { race = value; OnPropertyChanged("Race"); } }

        public string Classe { get => classe; set { classe = value; OnPropertyChanged("Classe"); } }

        public string Alignement { get => alignement; set { alignement = value; OnPropertyChanged("Alignement"); } }

        public string Divinite { get => divinite; set { divinite = value; OnPropertyChanged("Divinite"); } }

        public string Armure { get => armure; set { armure = value; OnPropertyChanged("Armure"); } }

        public string Arme { get => arme; set { arme = value; OnPropertyChanged("Arme"); } }

        public int PoidsKg { get => poidsKg; set { poidsKg = value; OnPropertyChanged("PoidsKg"); } }

        public int Niveau { get => niveau; set { niveau = value; OnPropertyChanged("Niveau"); } }

        public int Age { get => age; set {age = value; OnPropertyChanged("Age"); } }

        public int Taille { get => taille; set { taille = value; OnPropertyChanged("Taille"); } }

        public int Force { get => force; set { force = value; OnPropertyChanged("Force"); } }

        public int Dexterite { get => dexterite; set { dexterite = value; OnPropertyChanged("Dexterite"); } }

        public int Constitution { get => constitution; set { constitution = value; OnPropertyChanged("Constitution"); } }

        public int Intelligence { get => intelligence; set { intelligence = value; OnPropertyChanged("Intelligence"); } }

        public int Sagesse { get => sagesse; set { sagesse = value; OnPropertyChanged("Sagesse"); } }

        public int Charisme { get => charisme; set { charisme = value; OnPropertyChanged("Charisme"); } }

        public int BonusReflex { get => bonusReflex; set { bonusReflex = value; OnPropertyChanged("BonusReflex"); } }

        public int BonusVigueur { get => bonusVigeur; set { bonusVigeur = value; OnPropertyChanged("BonusVigueur"); } }

        public int BonusVolonte { get => bonusVolonte; set { bonusVolonte = value; OnPropertyChanged("BonusVolonte"); } }


        #region 
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
