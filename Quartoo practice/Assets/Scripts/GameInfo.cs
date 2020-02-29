public class GameInfo
{
    // N = Network, E = Easy AI Game, H = Hard AI Game, S = Story Mode
    public static char gameType;

    // 1 = player 1 (host of network game; human of quick game) 2 = player 2 (player who joins network game; ai of quick game)
    public static int selectPieceAtStart;

    public static float musicVolume = .5f;

    public static float soundFXVolume = .5f;

    public void meFirst()
    {
        selectPieceAtStart = 1;
    }

    public void themFirst()
    {
        selectPieceAtStart = 2;
    }
}