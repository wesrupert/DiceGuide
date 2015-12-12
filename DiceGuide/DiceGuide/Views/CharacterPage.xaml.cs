using DiceGuide.Models;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace DiceGuide
{
    /// <summary>
    /// The page that holds a character's information.
    /// </summary>
    public sealed partial class CharacterPage : Page
    {
        public Character Character;

        public CharacterPage()
        {
            this.InitializeComponent();

            Character = Compendium.Instance.Characters.FirstOrDefault();
        }

    }
}
