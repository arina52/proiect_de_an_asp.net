using System.Data.Entity;
using culinaryConnect.BusinessLogic.Models.AdminDB;
using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.BusinessLogic.Models;
using culinaryConnect.Domain.Entities.Recipe;
using System.Collections.Generic;
using System;
namespace culinaryConnect.BusinessLogic.Seed
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<CulinaryContext>
    {
        protected override void Seed(CulinaryContext context)
        {
            context.Admins.Add(new AdminDB
            {
                AdminName = "daniel",
                AdminEmail = "daniel@gmail.com",
                // 123 password + 123 salt
                PasswordHash = "932F3C1B56257CE8539AC269D7AAB42550DACF8818D075F0BDF1990562AAE3EF"
            });


            context.Recipes.AddRange(new List<RecipeDB>
            {
             new RecipeDB
            {
                Title = "Supă de dovleac",

                AboutRecipe = new RecipeAboutDB
                {
                    CookingTime = "40 de minute",
                    Ingredients = "800g dovleac curățat și tăiat cuburi###1 ceapă medie###2 căței de usturoi###1 morcov###1 cartof###1 litru supă de legume sau apă###2 linguri ulei de măsline###Sare, piper – după gust",
                    Description = "O supă cremoasă și aromată, perfectă pentru serile răcoroase de toamnă. Se prepară ușor și este plină de savoare.",
                    Instructions = @"Într-o oală mare, călește ceapa și usturoiul tocate mărunt în ulei de măsline, timp de 2-3 minute.###
Adaugă morcovul, cartoful și dovleacul tăiate cuburi. Amestecă și lasă-le să se înmoaie ușor timp de 5 minute.###
Toarnă supa de legume (sau apă) peste legume, acoperă și fierbe la foc mediu timp de 25-30 de minute, până toate legumele sunt moi.###
Ia oala de pe foc și pasează totul cu un blender vertical până obții o cremă fină.###
Potrivește gustul cu sare, piper și, dacă vrei, adaugă puțin ghimbir ras.###
Pentru o textură mai bogată, adaugă smântâna și mai dă supa un clocot.###
Servește cu crutoane sau semințe de dovleac prăjite deasupra."
                },
                CategoryID = 1,
                CreatedDate = new System.DateTime(2025, 5, 5),
                AuthorID = 1,
                ImagePath = "recipe-1.jpg"
            },
                 new RecipeDB
    {
        Title = "Prăjitură cu afine",
        AboutRecipe = new RecipeAboutDB
        {
            CookingTime = "50 de minute",
            Ingredients = "3 ouă###150g zahăr###150g făină###100g unt topit###1 linguriță praf de copt###1 linguriță esență de vanilie###200g afine proaspete sau congelate",
            Description = "O prăjitură pufoasă și aromată, perfectă pentru deserturi ușoare sau gustări de după-amiază.",
            Instructions = @"Preîncălzește cuptorul la 180°C.###Bate ouăle cu zahărul până se deschid la culoare.###Adaugă untul topit și esența de vanilie.###Încorporează făina amestecată cu praful de copt.###Adaugă afinele la final, amestecând ușor.###Toarnă compoziția într-o tavă tapetată cu hârtie de copt.###Coace 35-40 minute.###Lasă la răcit și pudrează cu zahăr."
        },
        CategoryID = 2,
        CreatedDate = new DateTime(2025, 5, 9),
        AuthorID = 1,
        ImagePath = "recipe-2.jpg"
    },
    new RecipeDB
    {
        Title = "Steak de porc cu ceapă caramelizată",
        AboutRecipe = new RecipeAboutDB
        {
            CookingTime = "1 oră",
            Ingredients = "2 cotlete de porc###2 cepe mari###2 linguri zahăr brun###2 linguri oțet balsamic###2 linguri ulei###sare, piper, cimbru",
            Description = "Un preparat suculent și aromat, ideal pentru mesele consistente de prânz sau cină.",
            Instructions = @"Condimentează steak-urile cu sare, piper și cimbru.###Prăjește-le 4-5 minute pe fiecare parte.###Într-o altă tigaie, călește ceapa în ulei 10 minute.###Adaugă zahărul brun și oțetul balsamic.###Gătește până ceapa devine moale și caramelizată.###Servește steak-ul cu ceapa deasupra."
        },
        CategoryID = 3,
        CreatedDate = new DateTime(2025, 5, 9),
        AuthorID = 1,
        ImagePath = "recipe-3.jpg"
    },
    new RecipeDB
    {
        Title = "Pizza de casă",
        AboutRecipe = new RecipeAboutDB
        {
            CookingTime = "1 oră și 20 de minute",
            Ingredients = "300g făină###200ml apă caldă###1 plic drojdie uscată###2 linguri ulei###sos de roșii###mozzarella###salam, ciuperci, măsline (opțional)",
            Description = "O pizza făcută în casă, cu blat crocant și toppinguri după pofta inimii.",
            Instructions = @"Amestecă făina cu drojdia, apa și uleiul.###Frământă și lasă aluatul la dospit 1 oră.###Întinde aluatul într-o tavă unsă.###Adaugă sosul de roșii, brânza și restul ingredientelor.###Coace la 200°C timp de 15-20 de minute.###Servește caldă."
        },
        CategoryID = 4,
        CreatedDate = new DateTime(2025, 5, 9),
        AuthorID = 1,
        ImagePath = "recipe-4.jpg"
    },
    new RecipeDB
    {
        Title = "Somon cu legume",
        AboutRecipe = new RecipeAboutDB
        {
            CookingTime = "35 de minute",
            Ingredients = "2 fileuri de somon###1 dovlecel###1 ardei roșu###1 morcov###2 linguri ulei de măsline###lămâie###sare, piper, rozmarin",
            Description = "O rețetă sănătoasă și rapidă, bogată în omega-3 și vitamine.",
            Instructions = @"Așază fileurile de somon într-o tavă.###Condimentează cu sare, piper, rozmarin și stropește cu zeamă de lămâie.###Taie legumele felii subțiri și amestecă-le cu ulei și condimente.###Pune legumele lângă somon.###Coace totul la 180°C timp de 25-30 minute."
        },
        CategoryID = 3,
        CreatedDate = new DateTime(2025, 5, 9),
        AuthorID = 1,
        ImagePath = "recipe-5.jpg"
    },
    new RecipeDB
    {
        Title = "Clătite cu zmeură",
        AboutRecipe = new RecipeAboutDB
        {
            CookingTime = "30 de minute",
            Ingredients = "2 ouă###300ml lapte###150g făină###1 lingură zahăr###un praf de sare###unt pentru prăjit###200g zmeură###frișcă (opțional)",
            Description = "Clătite fine și aromate, servite cu zmeură proaspătă și frișcă – perfecte pentru mic dejun sau desert.",
            Instructions = @"Amestecă ouăle, laptele, făina, zahărul și sarea.###Lasă aluatul la odihnit 10 minute.###Prăjește clătitele în tigaie unsă cu unt.###Umple-le cu zmeură și, opțional, frișcă.###Rulează și servește imediat."
        },
        CategoryID = 2,
        CreatedDate = new DateTime(2025, 5, 9),
        AuthorID = 1,
        ImagePath = "recipe-6.jpg"
    }
            });
        
            context.SaveChanges();
        }
    }
}