# infos

Slice sind die drei Achsen  
Layer sind die Bildersätze (MRI, CT, verschiedene Fälle)  
brauchen wir noch Unterordnung (Fall/case) oder hat Layer Personen/Fall-Id, Datum und unterschiedliche "Technik"? 


## Klassen
- dmt_HeadController
- dmt_Layer
- dmt_SliceController
- dmt_Slices

### dmt_HeadController
Start
+ headpos
+ Layerlist  mit >Layer (new) 
  + init mit  
  >Layer>setLayer()

Update
+ ruft aktiven >Layer auf: updateSlices(v)
  + mit dem normalisierten Punkt v im Kopf aus Eingabe

### dmt_Layer
für jede Slice eine Liste von Bildern ImagesX, ImagesY, ImagesZ
drei Slices 

+ setLayer  
  >setslice()

+ setSlice
  + lädt die Bilder eines Layers in die drei Listen
  + berechnet steps für jeden Layer und Slice

+ updateSlice 
  + >slice.showSlice  zeigt das richtige Bild in jedem Slice an



### dmt_Slices

+ setLayerActive   
=> (noch nicht verwendet)

+ showSlice  
  + > dmt_headcontroller.update()für jede Slice aufgerufen   
    berechnet passendes Bild  
    ändert texture und versteckt sliceObject, wenn nicht im Kopf
    > ändert Position des Sliceobjekts


## ACHTUNG
in der SliceZ ist tiling x verkehrt (-1)  
alle anderen lassen sich mit tiling und offset einstellen   
=> wie speichern?

## kalibrieren
+ Einheitsquader anzeigen
+ input auf max/max/max stellen
+ alle drei offsets so einstellen, dass alle hinten/unten/links anstehen
+ dann tiling so einstellen, dass sie das andere Ende auch berühren
+ => Wert permanent speichern! (Gamesettings?)



# TODOS

1) start & Endbild für jeden Layer/Slice angeben
2) in images[] berücksichtigen
3) 
