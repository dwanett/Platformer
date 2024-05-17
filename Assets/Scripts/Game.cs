using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
   [SerializeField] private Player _player;

   private void OnEnable()
   {
      _player.Die += ReloadGameScene;
   }

   private void OnDisable()
   {
      _player.Die -= ReloadGameScene;
   }

   private void ReloadGameScene()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }
}
