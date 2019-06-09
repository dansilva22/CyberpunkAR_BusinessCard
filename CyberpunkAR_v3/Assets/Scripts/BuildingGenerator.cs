using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{
    public List<GameObject> blocks = new List<GameObject>();
    public List<GameObject> temp =  new List<GameObject>();
    public Transform spawn; 
    public int maxBlocks = 6;
    public int minBlocks = 3;
   // public GameObject block;
    // Start is called before the first frame update
    void Start()
    {
        
       // StartCoroutine("Generate");
    }

    // Update is called once per frame
    

   public  IEnumerator Generate()
    {
        //determine the height of the building
        int height = Random.Range(minBlocks,maxBlocks);
        spawn = transform.GetChild(0);
        for(int i = 0; i < height; i++)
        {
            //find a random block from the list of blocks
            int rndBlock = Random.Range(0,blocks.Count);

            GameObject curBlock = Instantiate(blocks[rndBlock],spawn.position,transform.rotation);
            //set parent of the object to this building
            curBlock.transform.SetParent(transform);
           // curBlock.transform.PositionInParentReference;
            //
            curBlock.transform.localScale = transform.localScale;
            //get the spawn point from the new block so that the next block has a position.
            spawn = curBlock.transform.GetChild(0);
            temp.Add(curBlock);
            Debug.Log(temp.Count + " items in list.");

        }
        

        return null;
    }
}
