using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static HRDLL.HRDLL;


namespace HRDLL
{
    public class HRDLL
    {
        public struct Employee
        {
            public string Name;
            public string HomePhone;
            public string MobilePhone;
            public DateTime Birthday;
            public DateTime HiringDate;

            public Employee(string Name, string HomePhone, string MobilePhone, DateTime Birthday, DateTime HiringDate)
            {
                this.Name = Name;
                this.HomePhone = HomePhone;
                this.MobilePhone = MobilePhone;
                this.Birthday = Birthday;
                this.HiringDate = HiringDate;
            }
        }

        public bool ValidName(string name)
        {
            // Έλεγχος αν όλες οι λέξεις αποτελούνται από γράμματα (μικρά ή κεφαλαία)
            string[] words = name.Split(' ');


            foreach (string word in words)

            {

                if (!Regex.IsMatch(word, @"^[A-Za-z]+$"))

                {
                    return false;

                }


            }


            return true;
        }


        /* 

        2) Η συνάρτηση αυτή δέχεται ως παράμετρο ένα κείμενο και επιστρέφει τιμή true ή false αν η παράμετρος αποτελεί έναν  

        αποδεκτό κωδικό πρόσβασης. Προϋποθέσεις κωδικού: α. Τουλάχιστον 12 χαρακτήρες, β. Συνδυασμός κεφαλαίων γραμμάτων,  

        πεζών γραμμάτων, αριθμών και συμβόλων (τουλάχιστον έναν χαρακτήρα από το καθένα), γ. Τα γράμματα να είναι λατινικοί  

        χαρακτήρες, δ. Να ξεκινάει από κεφαλαίο γράμμα και να τελειώνει με αριθμό. 

        */

        public bool ValidPassword(string password)

        {
            // Έλεγχος για τουλάχιστον 12 ψηφία
            if (password.Length < 12)
                return false;

            // Έλεγχος για πεζούς χαρακτήρες
            if (!Regex.IsMatch(password, "[a-z]"))
                return false;

            // Έλεγχος για κεφαλαίους χαρακτήρες
            if (!Regex.IsMatch(password, "[A-Z]"))
                return false;

            // Έλεγχος για ψηφία
            if (!Regex.IsMatch(password, "[0-9]"))
                return false;

            // Έλεγχος για σύμβολα
            if (!Regex.IsMatch(password, "[^a-zA-Z0-9]"))
                return false;

            // Έλεγχος για ένα κεφαλαίο γράμμα στην αρχή
            if (!char.IsUpper(password[0]))
                return false;

            // Έλεγχος για έναν αριθμό στο τέλος
            if (!char.IsDigit(password[password.Length - 1]))
                return false;

            return true;
        }



        // =================================================================================================================== 



        /*  

        3) Η μέθοδος αυτή δέχεται ως παράμετρο έναν κωδικό πρόσβασης και επιστρέφει τον κρυπτογραφημένο κωδικό με βάση τον  

        Κώδικα του Καίσαρα (Caesar’s Cipher), με αλφάβητο το ASCII (128 χαρακτήρες) και ολίσθηση κατά 5 θέσεις. Το  

        αποτέλεσμα αυτό επιστρέφεται μέσω της αντίστοιχης by ref παραμέτρου που δέχεται. 

        */


        public void EncryptPassword(string password, ref string encryptedPW)
        {
            foreach (char c in password)
            {
                // Έλεγχος αν ο χαρακτήρας είναι γράμμα
                if (char.IsLetter(c))
                {
                    // Μετατόπιση του χαρακτήρα κατά 5 θέσεις στο ASCII
                    char encryptedChar = (char)(c + 5);
                    // Αν ο χαρακτήρας υπερβαίνει το 'z' ή το 'Z', επιστρέφουμε στην αρχή του αλφαβήτου
                    if ((char.IsUpper(c) && encryptedChar > 'Z') || (char.IsLower(c) && encryptedChar > 'z'))
                    {
                        encryptedChar = (char)(c - (26 - 5));
                    }
                    encryptedPW += encryptedChar;
                }
                else
                {
                    // Αν δεν είναι γράμμα, δεν το μετατρέπουμε
                    encryptedPW += c;
                }
            }
        }


        /*  

        4) Η μέθοδος αυτή δέχεται ως κείμενο έναν αριθμό και ελέγχει αν αντιστοιχεί σε τηλέφωνο. Τα αποτελέσματα επιστρέφονται  

        μέσω των αντίστοιχων by refπαραμέτρων που δέχεται. Συγκεκριμένα, αν είναι σταθερό τηλέφωνο, επιστρέφει ως τύπο  

        τηλεφώνου (TypePhone) μηδέν (0) και ως πληροφορίες τηλεφώνου (InfoPhone) τη ζώνη που ανήκει ο αριθμός τηλεφώνου  

        (8 ζώνες). Αν είναι κινητό τηλέφωνο, επιστρέφει ως τύπο τον αριθμό ένα (1) και ως πληροφορίες την εταιρεία κινητής  

        τηλεφωνίας. Διαφορετικά, αν δεν είναι έγκυρος αριθμός τηλεφώνου, επιστρέφει ως τύπο τον αριθμό -1 και null στις  

        πληροφορίες. 

     */

        public void CheckPhone(string phoneNumber, ref int phoneType, ref string phoneInfo)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                phoneType = -1;
                phoneInfo = null;
                return;
            }

            string cleanedPhoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());

            if (cleanedPhoneNumber.Length == 10 && cleanedPhoneNumber[0] == '2')
            {
                phoneType = 0; // Landline
                phoneInfo = GetPhoneLocation(cleanedPhoneNumber);
            }
            else if (cleanedPhoneNumber.Length == 10 && cleanedPhoneNumber[0] == '6')
            {
                phoneType = 1; // Mobile
                phoneInfo = GetMobileCompany(cleanedPhoneNumber);
            }
            else
            {
                phoneType = -1; // Invalid
                phoneInfo = null;
            }
        }

        private string GetPhoneLocation(string phoneNumber)
        {
            switch (phoneNumber.Substring(0, 2))
            {
                case "21": return "Athens";
                case "22": return "Central Greece and the Aegean Islands";
                case "23": return "Central Macedonia and Florina";
                case "24": return "Thessaly and West Macedonia";
                case "25": return "East Macedonia and Thrace";
                case "26": return "West Greece, Ionian Island and Epirus";
                case "27": return "Peloponnese and Kythera";
                case "28": return "Crete";
                default: return "Unknown";
            }
        }

        private string GetMobileCompany(string phoneNumber)
        {
            switch (phoneNumber.Substring(0, 3))
            {
                case "697":
                case "698": return "Cosmote";
                case "694":
                case "695": return "Vodafone";
                case "690":
                case "693":
                case "699": return "Nova";
                default: return "Unknown";
            }
        }

        /*  

       5) Η μέθοδος αυτή δέχεται ως παραμέτρους τα στοιχεία ενός υπαλλήλου, και επιστρέφει την ηλικία του και τα χρόνια  

       προϋπηρεσίας στην εταιρεία. Τα αποτελέσματα αυτά επιστρέφονται μέσω των αντίστοιχων by ref παραμέτρων που δέχεται. 

    */

        public void InfoEmployee(Employee employee, ref int age, ref int yearsOfExperience)
        {
            DateTime currentDate = DateTime.Now;
            age = CalculateAge(employee.Birthday, currentDate);
            yearsOfExperience = CalculateYearsOfExperience(employee.HiringDate, currentDate);
        }

        private int CalculateAge(DateTime birthday, DateTime currentDate)
        {
            int age = currentDate.Year - birthday.Year;

            if (currentDate.Month < birthday.Month || (currentDate.Month == birthday.Month && currentDate.Day < birthday.Day))
            {
                age--;
            }

            return age;
        }

        private int CalculateYearsOfExperience(DateTime hiringDate, DateTime currentDate)
        {
            int yearsOfExperience = currentDate.Year - hiringDate.Year;

            if (currentDate.Month < hiringDate.Month || (currentDate.Month == hiringDate.Month && currentDate.Day < hiringDate.Day))
            {
                yearsOfExperience--;
            }

            return yearsOfExperience;
        }


        public int LiveInAthens(Employee[] Empls)
        {

            int count = 0;

            foreach (var emp in Empls)
            {
                int phoneType = -1;
                string phoneInfo = null;

                // Call CheckPhone function to determine phone location
                CheckPhone(emp.HomePhone, ref phoneType, ref phoneInfo);

                // If the phone location is Athens, increment the count
                if (phoneInfo == "Athens")
                {
                    count++;
                }
            }

            return count;
        }

    }
}
