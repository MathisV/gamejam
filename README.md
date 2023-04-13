<p align="left">
  <img src="https://github.com/MathisV/gamejam/blob/main/images/logo.png" width="350" title="logo">
</p>

Nous sommes fiers de vous présenter notre jeu pour la Game Jam organisée par EFFICOM Lille le 12 avril 2023.

**But du jeu :** 
Dans Shiba ADVENTURE, le joueur incarne un personnage qui a pour objectif de parcourir le plus de distance possible sans mourir.
<p align="center">
  <img src="https://github.com/MathisV/gamejam/blob/main/images/image.png" title="image en développement">
</p>
<p align="center">
  *Capture d’écran du jeu en cours de développement*
</p>

**Les obstacles :** 
Sur le parcours, il est possible de rencontrer divers obstacles ayant pour objectif de :

  - Ralentir le joueur
  - Tuer le joueur

Il existe plusieurs types d’obstacles ayant diverses interactions avec le joueur : 
  - Les obstacles de saut : 
<p align="center">
  <img src="https://github.com/MathisV/gamejam/blob/main/images/obstacles_saut.png">
</p>

Leurs but est de ralentir le joueur ou le gêner. Certains peuvent être déplacés, d’autres sont collés au terrain.

  - Obstacles d’action : 
<p align="center">
  <img src="https://github.com/MathisV/gamejam/blob/main/images/obstacle_action.png">
</p>
Durant la partie, il est possible de tomber sur des interrupteurs qui, une fois le joueur à portée d’activation, remplace son saut par l’activation de ce dernier.

Dans le cas où il est activé, le levier fait apparaître un pont couvrant le prochain trou présent sur le terrain. Cependant, il est tout à fait possible de ne pas l’utiliser et de continuer la course sans perdre.

  - Les obstacles mortels :
 
 Comme leur nom l’indiquent, ces obstacles font perdre le joueur. Les trous ou les tonneaux explosifs sont les seuls obstacles mortels.
<p align="center">
  <img src="https://github.com/MathisV/gamejam/blob/main/images/obstacles_tnt.png">
</p>

**Le Gameplay :**

Durant une partie, le seul bouton disponible permet de sauter. Le personnage avance donc tout seul. Chaque obstacle que le joueur n’arrive pas à éviter déplace le personnage de plus en plus vers la gauche de l’écran. La partie se termine s’il touche cette même bordure.

Un nombre de mètres parcouru est présent en haut à gauche de l’écran et sert de score pour le joueur. Le jeu garde en mémoire le meilleur score du joueur.

<p align="center">
  <table border="0">
    <tr>
      <td>
        <p align="center">
          <img src="https://github.com/MathisV/gamejam/blob/main/images/run.gif">
        </p>
        <p align="center">
          *Animation de course de Shiba*
        </p>
      </td>
      <td>
        <p align="center">
          <img src="https://github.com/MathisV/gamejam/blob/main/images/jump.gif">
        </p>
        <p align="center">
          *Animation de saut de Shiba*
        </p>
      </td>
    </tr>
  </table>
</p>
