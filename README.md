# POO_Project
Projet ECAM Programation orientée objet 2020
https://quentin.lurkin.xyz/courses/poo/projet2020/

Dans ce projet vous trouverez un ensemble de classes ainsi qu'une interface permettant de simuler un réseau électrique.
Au lancement de celui-ci, l'interface vous proposera de créer un réseau de toutes pieces ou de lancer une démonstration illustrant les différents éléments (centrales, consommateurs, lignes, météo, ...) qu'il est possible d'utiliser. Vous pouvez tester/remodeler ce réseau a votre guise.


ETAPES DE CREATION D'UN RESEAU:

  Créez une nouvelle centrale:
  
Entrez dans l'onglet 'p' du menu. L'interface vous guidera pour choisir les caractéristiques de la centrale. Lors de sa création, une centrale se voit attribuer un noeud de distribution ainsi qu'une batterie. Le noeud de recharge de la batterie est automatiquement lié à sa centrale, mais son noeud de distribution devra être connecté manuellement. Pour bien faire, veuillez connecter la centrale ainsi que sa batterie à un même noeud de concentration dans le réseau. (voir étape d'assemblage du réseau)


  Créez un nouveau consommateur:
  
Entrez dans l'onglet 'c' du menu. L'interface vous guidera pour choisir les caractéristiques du consommateur. Lors de sa création, un consommateur se voit attribuer un noeud de concentration. C'est celui-ci qu'il faudra relier au réseau (voir étape d'assemblage du réseau).


  Créez un magasin:
  
 Afin d'assurer aux consommateurs d'avoir toujours suffisamment de courant à disposition, vous pouvez ajouter un maasin via l'onglet 'p' du menu. (de la même facon que pour créer un centrale).


  Créez des noeuds de concentration/distribution intermédiaires:
  
Ceux-ci permettent de distribuer efficacement l'électricité dans tout le réseau. 
Idéalement, il devrait y avoir au minimum un noeud de concentration puis un noeud de distribution entre les centrales et les consommateurs. Rien n'empeche de connecter un consommateur directement à une centrale car ceux-ci possèdent leur propres noeuds, cependant vous aurez besoin d'un noeud de concentration intermédiaire pour pouvoir y connecter la batterie de la centrale. (Bien que la batterie ne doivent pas forcément être connectée au même noeud que sa centrale, c'est préférable).
Sachez que lignes dissipatives sont créés automatiquement avec chaque noeud de distribution. Cela permet d'éviter toute surcharge.


  Assemblez le réseau:
  
Après avoir créé les éléments de votre réseau, il est temps d'assembler le tout! Passez par l'onglet 'w' du menu. L'interface s'occupe de créer les lignes qui relieront les noeud entre eux. 
N'oubliez pas d'également connecter les batteries au réseau.


Fonctionnement général:

Les consommateurs possèdent un attribut 'power claimed' qui se propage dans tout le réseau à la recherche d'une centrale capable de lui fournir sa demande d'électricité. Pour tester le réseau, l'onglet 'm' permet de choisir manuellement la demande d'un consommateur. Sinon cette demande s'adapte seule en fonction de différents paramètres (météo, nombre de machines d'une entreprise, ...).
L'onglet 'u' du menu permet de mettre à jour les centrales. Celle-ci regardent sur leur ligne les différentes demande de courant et envoient leur production dans le réseau.Le surplus de production est envoyé dans la batterie de la centrale (si elle n'est pas pleine, le cas écheant cette énergie sera perdue dans les lignes de dissipation). 
Si la demande est nulle ou fort élevée, une centrale peut décider de s'arreter ou de démarrer. Les différents noeuds de concentration/distribution s'occupent eux même de répartir les demandes:
Le programme ira d'abord chercher le courant chez les centrale en route et propres telles que des panneaux solaire, avant de réclamer aux centrales en route plus poluantes/plus chères. Si ca ne suffit pas, les batteries alimentent également le réseau. Si ca ne suffit toujours pas, les centrales qui ne l'étaient pas se mettent en route. Finalement, si toutes les centrales sont en route et que le réseau est sous alimenté, les noeuds réclameront du courant au 'magasin' (si vous en avez connecté un au réseau).


Pour plus de lisibilité, le diagramme de classe est repris sur l'image suivante sans les détails de variables, propriétés et méthodes. Ceux-ci sont repris dans le document ClassDiagram.cd du projet

![Class Diagram POO_Project](https://user-images.githubusercontent.com/60742506/103483542-3876e700-4de8-11eb-9388-538cc202c090.png)
