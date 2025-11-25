using UnityEngine;

namespace BalloonPop
{

    public class PurseManager : MonoBehaviour
    {
        // tmp
        private int m_coins = 0;

        public int Coins
        {
            get { return m_coins; }
        }

        public void AddCoins(int amount)
        {
            m_coins += amount;
        }
    }

}