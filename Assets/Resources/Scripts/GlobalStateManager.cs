/*
 * Copyright (c) 2017 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;
using UnityEngine.UI;

public class GlobalStateManager : MonoBehaviour
{
    public static GlobalStateManager Instance { get; set; }

    private int _scrorePlayer1;
    private int _scrorePlayer2;

    public int ScoreP1 => _scrorePlayer1;
    public int ScoreP2 => _scrorePlayer2;

    [SerializeField] private Text scroreTxtPlayer1;
    [SerializeField] private Text scroreTxtPlayer2;


    private void Awake()
    {
        Instance = this;
    }

    public void PlayerDied(int playerNumber)
    {
        if(playerNumber == 1)
        {
            _scrorePlayer2++;
            scroreTxtPlayer2.text = _scrorePlayer2.ToString();
        }
        else if (playerNumber == 2)
        {
            _scrorePlayer1++;
            scroreTxtPlayer1.text = _scrorePlayer1.ToString();
        }
    }
}
