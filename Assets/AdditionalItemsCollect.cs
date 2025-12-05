using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalItemsCollect : MonoBehaviour
{
    // items.Add(new Item(1,20,"Magnet"));
    // items.Add(new Item(2,50,"Acceleration"));
    // items.Add(new Item(3,60,"ExtraLife"));
    // items.Add(new Item(4,80,"JetBoots"));
    // items.Add(new Item(5,100,"JetPack"));
    
    public ItemsController items;
    public Move ch;
    public Study_Player studyPlayer;
    
    void OnTriggerEnter(Collider other)
    {
        if (ch != null)
        {
            HandleItemCollection(other);
        }
        else if (studyPlayer != null)
        {
            HandleItemCollectionStudy(other);
        }
    }
    
    private void HandleItemCollection(Collider other)
    {
        if (other.tag == "Magnet")
        {
            if (PlayerPrefs.GetInt("!sound") == 0) ch.sound.play2();
            ch.particle[1].gameObject.SetActive(true);
            ch.particle[1].Play();
            other.gameObject.SetActive(false);
            PlayerPrefs.SetInt("Magnet", PlayerPrefs.GetInt("Magnet") + 1);
        }
        else if (other.tag == "Acceleration")
        {
            if (PlayerPrefs.GetInt("!sound") == 0) ch.sound.play2();
            ch.particle[1].gameObject.SetActive(true);
            ch.particle[1].Play();
            other.gameObject.SetActive(false);
            PlayerPrefs.SetInt("Acceleration", PlayerPrefs.GetInt("Acceleration") + 1);
        }
        else if (other.tag == "Heart")
        {
            if (PlayerPrefs.GetInt("!sound") == 0) ch.sound.play2();
            ch.particle[1].gameObject.SetActive(true);
            ch.particle[1].Play();
            other.gameObject.SetActive(false);
            PlayerPrefs.SetInt("ExtraLife", PlayerPrefs.GetInt("ExtraLife") + 1);
        }
        else if (other.tag == "JetPack")
        {
            if (PlayerPrefs.GetInt("!sound") == 0) ch.sound.play2();
            ch.particle[1].gameObject.SetActive(true);
            ch.particle[1].Play();
            other.gameObject.SetActive(false);
            PlayerPrefs.SetInt("JetPack", PlayerPrefs.GetInt("JetPack") + 1);
        }
        
        items.updateItems();
    }
    
    private void HandleItemCollectionStudy(Collider other)
    {
        if (other.tag == "Magnet")
        {
            if (PlayerPrefs.GetInt("!sound") == 0) studyPlayer.sound.play2();

            // studyPlayer.particle[1].gameObject.SetActive(true);
            // studyPlayer.particle[1].Play();
            
            var particleCopy = Instantiate(studyPlayer.particle[1].gameObject, other.gameObject.transform.position, other.gameObject.transform.rotation) as GameObject;
            particleCopy.gameObject.SetActive(true);
            particleCopy.GetComponent<ParticleSystem>().Play();

            other.gameObject.SetActive(false);
            PlayerPrefs.SetInt("Magnet", PlayerPrefs.GetInt("Magnet") + 1);
        }
        else if (other.tag == "Acceleration")
        {
            if (PlayerPrefs.GetInt("!sound") == 0) studyPlayer.sound.play2();

            // studyPlayer.particle[1].gameObject.SetActive(true);
            // studyPlayer.particle[1].Play();

            var particleCopy = Instantiate(studyPlayer.particle[1].gameObject, other.gameObject.transform.position, other.gameObject.transform.rotation) as GameObject;
            particleCopy.gameObject.SetActive(true);
            particleCopy.GetComponent<ParticleSystem>().Play();

            other.gameObject.SetActive(false);
            PlayerPrefs.SetInt("Acceleration", PlayerPrefs.GetInt("Acceleration") + 1);
        }
        else if (other.tag == "Heart")
        {
            if (PlayerPrefs.GetInt("!sound") == 0) studyPlayer.sound.play2();

            // studyPlayer.particle[1].gameObject.SetActive(true);
            // studyPlayer.particle[1].Play();

            var particleCopy = Instantiate(studyPlayer.particle[1].gameObject, other.gameObject.transform.position, other.gameObject.transform.rotation) as GameObject;
            particleCopy.gameObject.SetActive(true);
            particleCopy.GetComponent<ParticleSystem>().Play();

            other.gameObject.SetActive(false);
            PlayerPrefs.SetInt("ExtraLife", PlayerPrefs.GetInt("ExtraLife") + 1);
        }
        else if (other.tag == "JetPack")
        {
            if (PlayerPrefs.GetInt("!sound") == 0) studyPlayer.sound.play2();

            // studyPlayer.particle[1].gameObject.SetActive(true);
            // studyPlayer.particle[1].Play();

            var particleCopy = Instantiate(studyPlayer.particle[1].gameObject, other.gameObject.transform.position, other.gameObject.transform.rotation) as GameObject;
            particleCopy.gameObject.SetActive(true);
            particleCopy.GetComponent<ParticleSystem>().Play();

            other.gameObject.SetActive(false);
            PlayerPrefs.SetInt("JetPack", PlayerPrefs.GetInt("JetPack") + 1);
        }
        
        items.updateItems();
    }
}