using UnityEngine;
using TMPro;

public class EconomyManager : Singleton<EconomyManager> {

    private int coinCount = 0;
    private TMP_Text coinCountText;

    const string COIN_AMOUNT_TEXT = "Coin Amount Text";

    public void UpdateCoins() {

        coinCount++;

        if (coinCountText == null) {
            coinCountText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }

        coinCountText.text = coinCount.ToString("D3");
    }
}
