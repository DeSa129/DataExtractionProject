# DataExtractionProject

Sveiki,

Norėčiau pasidalinti savo minčių procesu darant šitą projektą,
visu pirma jog išvengčiau pirmo puslapio užkrovimo - sugeneravau url su skrydžio išvykimo diena, sugrįžimo diena, skrendančiu asmenų skaičiumi
taip išvengiau bent 1 requesto per skrydžių kombinacija.

kadangi užduotis prašo išgauti visas round-trip kombinacijas, reiškia negaliu tiesiog pasirinkti visu skrydžių iš užkrauto url-source,
reikia surasti konteinerius outbound-flights bei inbound-flights ir juos atskirai sudėti į sąrašus ir naudojant abu išgauti visu skrydžių kombinacijas

antroje užduoties dalyje prašote išgauti taxes, kurie yra antrame skrydžio pirkimo formos etape.
čia ir susidūriau su nesklandumais kadangi pasirinkau užduotį daryti su HtmlAgilityPack, jis neturi naršyklės variklio dėl to pasirinkti puslapyje skrydžių bei
nusigauti į antra formos etapą buvo neįmanoma ( na bent nepavyko man rasti, tikrai gali būti yra būdas bet tiesiog nežinau), todėl tiesiog padariau HardCode išeitį
nes pora kart rankiniu būdu pamėginęs supratau jog taxes yra pastovus per žmonių skaičių nepriklausomai nuo mokamos sumos.

nors mano manymu idealus sprendimas butu susirasti vieta kurioje mokesčiai yra aprašomi, prieš išgaunant skrydžius sugeneruoti formule mokesčių apskaičiavimui
ir taip sutaupyti dideli skaičių requestu

pabaigai,
jeigu daryčiau šita užduotį iš naujo rinkčiausi Selenium, jau buvo mintys kilusios perdaryti visa projektą. bet HtmlAgilityPack yra apie 4-5 kart greitesnis todėl palikau taip,
bet mano manymu HtmlAgilityPack trūksta funkcionalumo tokiem projektam.
