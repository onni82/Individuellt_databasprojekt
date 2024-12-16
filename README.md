# Individuellt databasprojekt
## Om uppgiften

Detta är den sista praktiska uppgiften i kursen. Det är en ganska fri uppgift där du ska bevisa att du kan bygga upp mer komplexa system med databaser. Uppgiften bygger vidare på labb 2 och 3.

## Grundläggande kriterier

### För `G` måste följande uppfyllas:

- Anropa databasen med både SQL och en ORM
- Konstruera väl fungerande SQL-kod
- Skapa en tydlig, användbar och stabil databasmodell

### För `VG` måste du även:

- Skriva en reflektion där du nyanserat/kritiskt resonerar kring och motiverar den databasmodell du har tagit fram. Du måste även nyanserat resonera kring prestanda och lämplighet i den SQL-kod du har producerat.
    
    Denna reflektion skickar du in som en PDF eller textfil tillsammans med din inlämning, filnamn kan vara: Resonemang.pdf
    

## Projekt

I detta projekt ska du bygga klart en helt fungerande applikation för den fiktiva skola du jobbat med i de senaste labbarna. Du ska alltså skapa en Consol-applikation som skolan kan använda och som har den funktionalitet som efterfrågas nedan.

### Funktioner i programmet:

Här följer de funktioner du ska bygga i ditt program. 

<aside>
➡️ Det måste finnas en meny där man kan välja att visa olika data som efterfrågas av skolan. (Console)

</aside>

<aside>
➡️ Skolan vill kunna ta fram en översikt över all personal där det framgår namn och vilka befattningar de har samt hur många år de har arbetat på skolan. Administratören vill också ha möjlighet att spara ner ny personal. (SQL i SSMS)

</aside>

<aside>
➡️ Vi vill spara ner elever och se vilken klass de går i. Vi vill kunna spara ner betyg för en elev i varje kurs de läst och vi vill kunna se vilken lärare som satt betyget. Betyg ska också ha ett datum då de satts. (SQL i SSMS)

</aside>

<aside>
➡️ Hur många lärare jobbar på de olika avdelningarna? (EF i VS)

</aside>

<aside>
➡️ Visa information om alla elever (EF i VS)

</aside>

<aside>
➡️ Visa en lista på alla (aktiva) kurser (EF i VS)

</aside>

<aside>
➡️ Hur mycket betalar respektive avdelning ut i lön varje månad? (SQL i SSMS)

</aside>

<aside>
➡️ Hur mycket är medellönen för de olika avdelningarna? (SQL i SSMS)

</aside>

<aside>
➡️ Skapa en Stored Procedure som tar emot ett Id och returnerar viktig information om den elev som är registrerad med aktuellt id. (SQL i SSMS)

</aside>

<aside>
➡️ Sätt betyg på en elev genom att använda Transactions ifall något går fel. (SQL i SSMS)

</aside>

<aside>
⚙ Extra utmaningar
1. Visa information om en elev, vilken klass hen tillhör och vilken/vilka lärare hen har samt vilka betyg hen har fått i en specifik kurs. (SQL)
2. Skapa en View som visar alla lärare och vilka utbildningar de ansvarar för. (SQL i SSMS)
3. Uppdatera/korrigera en elevs information via kod. (EF i VS)

</aside>

# Din inlämning

- Skicka in uppgiften I Canvas
- Skicka in en textfil som innehåller länken till ett Github-repo med din kod + en SQL-fil med din databas
- Om du satsar på VG även en PDF eller textfil med din reflektion enligt ovan.