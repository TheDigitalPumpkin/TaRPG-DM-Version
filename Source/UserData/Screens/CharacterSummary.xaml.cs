﻿using RPGproject.Source.CharacterCreation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace RPGproject.Source.UserData.Screens
{
    public struct CharacterEdit
    {
        Character character;
        int characterId;

        public CharacterEdit(Character character, int ID)
        {
            this.character = character;
            characterId = ID;
        }
    }

    public sealed partial class CharacterSummary : Page
    {
        private Character viewedCharacter;

        public CharacterSummary()
        {
            this.InitializeComponent();
        }

        void DisplayCharacterSummary(Character character)
        {
            CharacterName.Text = character.Name;
            CharacterAge.Text = character.Age.ToString();
            CharacterRace.Text = character.CharacterRace.Name;
            CharacterClass.Text = character.CharacterClass.Name;
            CharacterHeight.Text = character.Height;
            CharacterWeight.Text = character.GetFormattedWeight;
            StrengthAtt.Text = character.Attributes.Find(x => x.Name == "Strength").Value.ToString();
            DexterityAtt.Text = character.Attributes.Find(x => x.Name == "Dexterity").Value.ToString();
            ConstitutionAtt.Text = character.Attributes.Find(x => x.Name == "Constitution").Value.ToString();
            IntelligenceAtt.Text = character.Attributes.Find(x => x.Name == "Intelligence").Value.ToString();
            WisdomAtt.Text = character.Attributes.Find(x => x.Name == "Wisdom").Value.ToString();
            CharismaAtt.Text = character.Attributes.Find(x => x.Name == "Charisma").Value.ToString();

            List<int> attMods = character.AttributeModifiers;

            StrMod.Text = "(" + attMods.ElementAt(0).ToString() + ")";
            DexMod.Text = "(" + attMods.ElementAt(1).ToString() + ")";
            ConMod.Text = "(" + attMods.ElementAt(2).ToString() + ")";
            IntMod.Text = "(" + attMods.ElementAt(3).ToString() + ")";
            WisMod.Text = "(" + attMods.ElementAt(4).ToString() + ")";
            ChaMod.Text = "(" + attMods.ElementAt(5).ToString() + ")";
        }

        private async void DisplayDeleteCharacterConfirmation()
        {
            ContentDialog confirmation = new ContentDialog
            {
                Title = "Delete character",
                Content = "Are you sure you want to delete this character? This cannot be undone.",
                PrimaryButtonText = "Yes",
                CloseButtonText = "No"
            };

            ContentDialogResult result = await confirmation.ShowAsync();

            if(result == ContentDialogResult.Primary)
            {
                CharacterDB.DeleteCharacterbyID(viewedCharacter);
                this.Frame.Navigate(typeof(CreatedCharactersPage));
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter is Character)
            {
                viewedCharacter = (Character)e.Parameter;
                DisplayCharacterSummary((Character)e.Parameter);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if(this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        private void EditCharacter_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreateCharacterPageOne), viewedCharacter);
        }

        private void DeleteCharacter_Click(object sender, RoutedEventArgs e)
        {
            DisplayDeleteCharacterConfirmation();
        }
    }
}
