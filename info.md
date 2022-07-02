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
+ initialisiert 3 Slices => in Slicelist gespeichert  

Update
+ ruft Slices auf ShowSlice

### dmt_Layer
set  
=>
lädt die Bilder eines Layers in eine Liste 

### dmt_SliceController
=> noch nicht verwendet
Verwaltung der dargestellten Layer?!


### dmt_Slices
addLayer  
=> 

setLayerActive   
=> (noch nicht verwendet)

showSlice  
=> dmt_headcontroller.update()
für jede Slice aufgerufen  
berechnet passendes Bild  
ändert texture und versteckt sliceObject, wenn nicht im Kopf




# TODOS

beim Laden der Layer die Grössen der Bilder auslesen
=> daraus die Skalierung der Slices berechnen 
Problem: beim importern werden Bilder skaliert (512/1024)
Originaldaten eingeben!

646 x 916 

