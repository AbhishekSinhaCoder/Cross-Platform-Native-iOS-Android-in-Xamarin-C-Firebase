using System;
using System.Collections.Generic;



namespace Groceries
{
    public static class CompareLists
    {
        public static List<GroceryListClass> Compare(List<GroceryListClass> listA, 
                                                     List<GroceryListClass> listB)
        {
            List<GroceryListClass> combinedListsLST = new List<GroceryListClass>();

            // first, if there is a list in one that is not in other, just copy it 
            foreach (GroceryListClass a in listA)
            {
                foreach (GroceryListClass anyList in listB)
                {
                    if (a.Name == anyList.Name)
                        goto ContinueLoop;
                }
                combinedListsLST.Add(a);

            ContinueLoop:;
            }

            // do the same for B
            foreach (GroceryListClass b in listB)
            {
                foreach (GroceryListClass anyList in listA)
                {
                    if (b.Name == anyList.Name)
                        goto ContinueLoop;
                }
                combinedListsLST.Add(b);

            ContinueLoop:;
            }


            // so rmove the unique ones so we can compare others 
            foreach (GroceryListClass any in combinedListsLST)
            {
                if (listA.Contains(any))
                    listA.Remove(any);
                if (listB.Contains(any))
                    listB.Remove(any);
            }

            // now lt's compare the similar lists against each other
            foreach (GroceryListClass anyListA in listA)
            {
                List<ItemClass> thisListResultItems = new List<ItemClass>();

                GroceryListClass counterPartList = new GroceryListClass();
                DateTime combinedTime = DateTime.UtcNow;

                // first let's check the deleted status
                foreach (GroceryListClass anyListB in listB)
                {
                    if (anyListB.Name == anyListA.Name)
                    {
                        counterPartList = anyListB;
                        break;
                    }
                }

                // then let's check the items
                foreach (ItemClass anyItem in anyListA.Items)
                {
                    // let's compare items from one list to anothert
                    foreach (ItemClass counterItem in counterPartList.Items)
                    {
                        if (anyItem.Name == counterItem.Name)
                        {
                            // item exists both sides, let's decide which one to add
                            if (DateTime.Parse(anyItem.Time) > DateTime.Parse(counterItem.Time))
                                thisListResultItems.Add(anyItem);
                            else
                                thisListResultItems.Add(counterItem);

                            goto ContinueHere;
                        }
                    }
                    // if we reach here, non of the names have matchedd
                    thisListResultItems.Add(anyItem);
                ContinueHere:;
                }
                // by now all items of this LIST is resolved, now, let's deal with counter part


                foreach (ItemClass anyCounterItem in counterPartList.Items)
                {
                    // let's compare items from counter to the main
                    foreach (ItemClass anyItem in anyListA.Items)
                    {
                        if (anyCounterItem.Name == anyItem.Name)
                        {
                            // item exists both sides, we have already added that, let's drop out
                            goto ContinueHere;
                        }
                    }
                    // if we reach here, non of the names have matched
                    thisListResultItems.Add(anyCounterItem);

                ContinueHere:;
                }


                // this shopping class now contains all of the similar ones and uniques ones
                combinedListsLST.Add(new GroceryListClass
                {
                    Name = anyListA.Name,
                    Owner = anyListA.Owner,
                    Items = thisListResultItems
                });

            }

            return combinedListsLST;
        }
    }
}
