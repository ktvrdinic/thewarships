public class User
{
    //public string uid;
    public string username;
    public string noOfShips;
    public string score;

    public User()
    {

    }

    public User(string username, int noOfShips, int score)
    {
      //  this.uid = uid;
        this.username = username;
        this.noOfShips = noOfShips.ToString();
        this.score = score.ToString();
    }
}