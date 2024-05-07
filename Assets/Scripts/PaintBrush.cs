using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Haptics;

public class PaintBrush : MonoBehaviour
{
    public int hits;
    public GameObject particles;
    public float particleLifetime = 2f;
    private GameObject currParticleObject;
    private GameObject lastParticleObject;
    [SerializeField] private Palette _palette;
    
    [SerializeField] private HapticClip _hapticClip;
    private HapticClipPlayer _hapticPlayer;

    void Start()
    {
        hits = 0;
        _hapticPlayer = new(_hapticClip);
    }
    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        Color color = Color.green;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            color = Color.red;
            var target = hit.transform.gameObject.GetComponent<GhostStrokeTarget>();
            if (target != null)
            {
                // Debug.Log("Found a target!");
                if (target.Hit(_palette.currColor))
                {
                    hits++;
                    UI.manager.IncreaseScore(1);
                    _hapticPlayer.Play(Controller.Right);
                }
                //DestroyLastParticle();
                // Instantiate currParticleObject.
                // currParticleObject = Instantiate(particles, hit.point, Quaternion.identity);
                // if (lastParticleObject != null)
                // {
                //     // Set lastParticleObject to currParticleObject.
                //     lastParticleObject = currParticleObject;
                // }
                // Destroy(lastParticleObject, particleLifetime);
                
            }
        }
        Debug.DrawRay(transform.position, transform.forward * 100, color);
    }

    void DestroyLastParticle()
    {
        // Check if the lastParticleObject exists and is not already destroyed
        if (lastParticleObject != null && lastParticleObject.activeSelf)
        {
            // Destroy the last instantiated particle system
            Destroy(lastParticleObject);
        }
    }
}
