using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Haptics;

public class PaintBrush : MonoBehaviour
{
    // public int hits;
    public GameObject particles;
    public float particleLifetime = 2f;
    private GameObject currParticleObject;
    private GameObject lastParticleObject;
    [SerializeField] private PaletteMerge _palette;
    //[SerializeField] private Palette _palette;
    
    [SerializeField] private HapticClip _hapticClip;
    private HapticClipPlayer _hapticPlayer;

    void Start()
    {
        // hits = 0;
        _hapticPlayer = new(_hapticClip);
    }
    // Update is called once per frame
    void Update()
    {
        // Color color = Color.green;
        if (Physics.Raycast(transform.position, transform.forward, out var hit))
        {
            // color = Color.red;
            var target = hit.transform.gameObject.GetComponent<GhostStrokeTarget>();
            if (target != null)
            {
                if (target.Hit(_palette.currColor))
                {
                    // hits++;
                    // UI.manager.IncreaseScore(1);
                    _hapticPlayer.Play(Controller.Right);
                }

            }
        }
        // Debug.DrawRay(transform.position, transform.forward * 100, color);
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
