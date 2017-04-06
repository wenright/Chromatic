using UnityEngine;
using UnityEngine.Advertisements;

public class AdvertisementManager : MonoBehaviour
{
  public void Start()
  {
    // TODO only play an ad after every 3 deaths or so
    if (Advertisement.IsReady())
    {
      Advertisement.Show();
    }
  }
}