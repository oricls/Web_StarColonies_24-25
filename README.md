# Star Colonies

Projet d'application web pour l'UE19. Le projet prend la forme d'une solution .NET découpée en trois projets :

- un projet d'application web Razor Page
- un projet de bibliothèque modélisant le domaine.
- un projet d'infrastructures, ce dernier reprenant notamment les éléments propres à EntityFrameworkCore.

La suite du document sera à compléter par vos soins.

## Membres de l'équipe
- Alex Tadino ([Q210115](https://git.helmo.be/Q210115))
- Maximilien Withof ([Q220271](https://git.helmo.be/Q220271))
- Oriane Clesse ([Q220252](https://git.helmo.be/Q220252))

Responsable du déploiement : Maximilien Withof
- URL : https://ue19.cg.helmo.be/Q220271


## Construction de la solution

### Préalables
- .NET SDK (version 8.0 ou supérieure)
- Rider (vers.2025.1) ou autre IDE
- Git
- Accès à la base de données SQL Server d'Helmo

### Instructions de construction et d'exécution
1. Accédez à votre espace de travail
```bash
cd votre/espace/de/travail/
```
2. Créez un nouvel emplacement pour le projet
```bash
mkdir star_colonies
```
3. Accédez au nouvel emplacement du projet
```bash
cd star_colonies
```
4. Clonez le dépôt dans le nouvel emplacement du projet
```bash
git clone https://git.helmo.be/students/info/q220271/star-colonies.git
```
5. Accédez au dossier du projet cloné
```bash
cd star-colonies
```
6. Vérifiez que votre connexion à la base de données est correctement configurée
    - Ouvrez le fichier StarColonies.Web/appsettings.Development.json
    - Modifiez la chaîne de connexion si nécessaire avec vos identifiants :
```bash
"ConnectionStrings": {
  "DefaultConnection": "Data Source=asterix-intra.cg.helmo.be,11433;User Id=VOTRE_ID; Password=VOTRE_MOT_DE_PASSE; Initial Catalog=VOTRE_BASE; Encrypt=false"
}
```
7. Restaurez les dépendances et compilez la solution
```bash
dotnet restore
dotnet build
```
8. Accédez au projet web
```bash
cd StarColonies.Web
```
9. Exécutez l'application
```bash
dotnet run
```
10. Ouvrez votre navigateur et accédez à l'URL indiquée dans la console


## Fonctionnalités implémentées
- US1 : totalement achevée
- US2 : totalement achevée
- US3 : totalement achevée
- US4 : totalement achevée
- US5 : totalement achevée
- US6 : totalement achevée
- US7 : totalement achevée
- US8 : totalement achevée
- US9 : totalement achevée
- US10 : totalement achevée
- US11 : totalement achevée
- US12 : totalement achevée
- Bonus : totalement achevée

**TODO :** pour chaque US décrite dans l'énoncé, indiquez son état d'avancement (non-faite, débutée, partiellement achevée, totalement achevée). Quand une US est débutée ou partiellement achevée, indiquez en quelques mots ce qui manque selon vous.

## Données de connexion
### Administrateur
- **Email** : admin@admin.com , **Mdp** : Test_12345

### Colon
- **Email** : mira.nova@example.com , **Mdp** : Motdepasse123!
- **Email** : alex.striker@example.com, **Mdp** : Motdepasse123!

## Éléments techniques notables
Biblitohèques utilisées : 
- Chart.JS
