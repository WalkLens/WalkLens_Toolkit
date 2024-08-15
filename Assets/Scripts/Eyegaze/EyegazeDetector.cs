using UnityEngine;
using MixedReality.Toolkit;
using Photon.Pun;
using MRTK.Tutorials.MultiUserCapabilities;
using RealityCollective.Extensions;
using System.Collections;

public class EyegazeDetector : MonoBehaviour
{
    public Material redMaterial, whiteMaterial;
    private MeshRenderer eyegazedMesh;
    private PhotonUser photonUser;
    private bool isUIActivated = false;

    private void Start()
    {
        photonUser = this.gameObject.GetComponent<PhotonUser>();
        // eyegazedMesh = this.GetComponentInChildren<MeshRenderer>();
    }

    void Update()
    {
        Ray ray = new Ray(photonUser.transform.position, photonUser.transform.transform.forward);
        RaycastHit hit;

        // 30 : Photon User
        int layerMask = 1 << 30;

        if (!isUIActivated)
        {
            // if Ray hits Photon User, Instantiate InfoUI
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                //EyegazeUIManager.main.ActivateEyegazeUI(hit);
                eyegazedMesh = hit.collider.GetComponentInChildren<MeshRenderer>();
                eyegazedMesh.material = redMaterial;
                StartCoroutine(Activate(hit));
                isUIActivated = true;
            }
        }
        else
        {
            if (!Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                EyegazeUIManager.main.DeactivateEyegazeUI();
                eyegazedMesh.material = whiteMaterial;
                isUIActivated = false;
            }
        }
    }

    private IEnumerator Activate(RaycastHit hit)
    {
        EyegazeUIManager.main.ActivateEyegazeUI(hit);
        EyegazeUIManager.main.DeactivateEyegazeUI();
        yield return new WaitForSeconds(0.1f);
        EyegazeUIManager.main.ActivateEyegazeUI(hit);
    }

    // public void OnEyegazeEnter()
    // {
    //     if (!isUIActivated)
    //     {

    //         EyegazeUIManager.main.ActivateEyegazeUI(this.gameObject);
    //         EyegazeUIManager.main.DeactivateEyegazeUI();
    //         EyegazeUIManager.main.ActivateEyegazeUI(this.gameObject);
    //         eyegazedMesh.material = redMaterial;
    //         isUIActivated = true;
    //     }
    // }

    // public void OnEyegazeExit()
    // {
    //     EyegazeUIManager.main.DeactivateEyegazeUI();
    //     eyegazedMesh.material = whiteMaterial;
    //     isUIActivated = false;
    // }

}