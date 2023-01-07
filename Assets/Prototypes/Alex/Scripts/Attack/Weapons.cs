public abstract class Weapon
{
    private string[] _shape;
}

public class Fists : Weapon
{
    private string[] _shape = new string[] {
        // top down view R=Robot
        "OXO",
        "XRX",
        "OXO"
    };

}