using Unity.UIWidgets.material;
using Unity.UIWidgets.ui;

namespace NotePro
{
    public class Utils
    {
        public static Color PriorityColor(int index)
        {
            switch (index)
            {
                case 0:
                    return Colors.green;
                case 1:
                    return Colors.yellow;
                case 2:
                    return Colors.red;
            }

            return Colors.yellow;
        }

        public static string PriorityText(int index)
        {
            switch (index)
            {
                case 0:
                    return "!";
                case 1:
                    return "!!";
                case 2:
                    return "!!!";
            }

            return "!!";
        }
    }
}