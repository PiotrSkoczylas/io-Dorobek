# coding=utf-8

import PyPDF2
import json



def slowa_kluczowe(tekst):
    result=[]
    x=tekst.split()
    for i, wyraz in enumerate(x):
        if 'KEYWORDS' in wyraz.upper() or 'KLUCZOWE' in wyraz.upper():
            for j in range(5):
                try:
                    result.append(x[i+j+1])
                except:
                    break
    return result



def DOI(tekst):
    doi=''

    lista_wyrazow = tekst.split()
    poprzedni_doi=False

    for wyraz in lista_wyrazow[:100]:
        if wyraz.upper() in 'ABSTRACT:':
            break

        #Wyszukiwanie DOI
        if poprzedni_doi==True:
            doi=doi+wyraz
            poprzedni_doi=False

        elif wyraz.upper() in 'DOI: ' or 'DOI' in wyraz.upper():
            poprzedni_doi=True
            doi=doi+wyraz

    return doi



def Wyszukiwanie_autorow(tekst):
    tekst = tekst.replace('-', ' ')
    lista_wyrazow = tekst.split()
    autorzy = ''
    poprzedni_imie = False
    poprzedni_nazwisko=False

    for wyraz in lista_wyrazow[:100]:
        
        if len(wyraz)>=3:
            if wyraz[-2:].isnumeric():
                wyraz=wyraz[:-2]    
            elif wyraz[-1].isnumeric():
                wyraz=wyraz[:-1]
            elif wyraz[-3:-1].isnumeric():
                wyraz=wyraz[:-3]+wyraz[-1]
            elif wyraz[-2].isnumeric():
                wyraz=wyraz[:-2]+wyraz[-1]

        if wyraz.upper() in 'ABSTRACT:':
            break
       
        #Wyszukiwanie autora
        czy_imie = False
        czy_nazwisko = False
        if wyraz[0].isupper():
            sciezka_pliku = "python_imiona\\"+wyraz[0]+"_imiona.csv"
            try:
                plik = open(sciezka_pliku, 'r', encoding='utf8')
            except Exception:
                continue
            imiona = plik.read()
            for imie in imiona.split('\n'):
                if imie == wyraz.replace(',', '').upper():
                    autorzy += (" "+wyraz)
                    poprzedni_imie = True
                    czy_imie = True
                    break
            plik.close()

            if not czy_imie:
                sciezka_pliku = "python_nazwiska\\"+wyraz[0]+"_nazwiska.csv"
                try:
                    plik = open(sciezka_pliku, 'r', encoding='utf8')
                except Exception:
                    continue
                nazwiska = plik.read()
                for nazwisko in nazwiska.split('\n'):
                    if nazwisko == wyraz.replace(',', '').upper():
                        autorzy += (" "+wyraz)
                        czy_nazwisko = True
                        poprzedni_nazwisko = True
                        break
                plik.close()

        if(czy_nazwisko == False and poprzedni_nazwisko == True):

            poprzedni_nazwisko = False
            if (czy_imie == False):          
                poprzedni_imie = False
                autorzy += ','

            elif (czy_imie == True):
                poprzedni_imie = True
                
                i=len(autorzy)-1
                while(autorzy[i]!=' '):
                    i-=1
                autorzy=autorzy[:i]+','+autorzy[i:]

    return autorzy



def dzielenie_po_wielkich_literach(tekst):
    i = 1
    while(i < len(tekst)):
        if (tekst[i].isupper() and not tekst[i-1].isupper() and not tekst[i-1].isspace()):
            tekst = tekst[:i]+' '+tekst[i:]
        i += 1
    return tekst



def funkcja_pobierajaca_tekst(sciezka, metoda):
    if metoda == "PyPDF2":
        try:
            with open(sciezka, 'rb') as plik:
                pdf_reader = PyPDF2.PdfFileReader(plik)
                result = [[pdf_reader.getPage(x).extractText() for x in range(2)], [
                    dzielenie_po_wielkich_literach(pdf_reader.getPage(x).extractText()) for x in range(2)]]

        except Exception as a:
            print("WYSTĄPIŁ BŁĄD!!! \n W funkcja_pobierajaca_tekst!")
            print(a)
            return -1

        return result

    elif 1 == 1:
        pass



def wyszukiwanie_tytulu(tekst):  # text podzielony na wyrazy
    na_linie = tekst.split('\n')
    kandydaci = {}
    nr_kandydata = 0
    na_linie2 = na_linie[:20]   # zawężenie do 20 pierwszych linii

    for linia in na_linie2:

        kandydaci[linia] = 0

        if 'abstract'.upper() in linia.upper(): # or 'introduction'.upper() in linia.upper():
            break

        try:
            if linia[-1] != '.':
                kandydaci[linia] += 1
        except Exception as a:
            continue

        nr_kandydata += 1

        if nr_kandydata < 12: 
            kandydaci[linia] += 1

        if len(linia) > 10:
            kandydaci[linia] += 1

        if linia[0].isupper():
            kandydaci[linia] += 1

        i = 0
        autor = False
        for wyraz in linia.split():
            if wyraz[0].isupper():
                i += 1
            elif len(wyraz) <= 3 or wyraz in ['about', 'under', 'below', 'from', 'with', 'without', 'within', 'between',
                                              'wohin', 'woher', 'nach', 'oder', 'über',
                                              'obok', 'przy', 'stąd', 'dotąd', 'dokąd', ]:
                i = 0

            if wyraz in Wyszukiwanie_autorow(linia).split():
                autor = True

        if i >= 3:
            kandydaci[linia] += 1

        if autor:
            kandydaci[linia] -= 2

        lista_slow1 = ['DOI', 'DOI:', 'VOL.', 'VOL', 'Copyright', 'copyrigth', 'http', 'authors', 'keywords', 'e-mail',
                       'abstract', '©', 'ISSN', 'ISBN']
        if any(slowo.upper() in linia.upper() for slowo in lista_slow1):
            # print('!!!')
            kandydaci[linia] -= 4

        lista_slow2 = ['analysis', 'research', 'overview', 'analiz', 'badanie', 'badań', 'przegląd', 'investigetion',
                       'dochodzenie', 'experiment', 'eksperyment']
        if any(slowo.upper() in linia.upper() for slowo in lista_slow2):
            kandydaci[linia] += 1

        cyfr = 0
        for znak in linia:
            if znak.isnumeric():
                cyfr += 1
                if cyfr >= 5:
                    kandydaci[linia] -= 1
                    break
            else:
                cyfr = 0

    return kandydaci



def slowniki_na_liste(lista_slownikow):

    wyniki = []
    for i in range(15, 0, -1):
        wyniki.append(str(i))
        for j in range(len(lista_slownikow)):
            for haslo in lista_slownikow[j].keys():
                if lista_slownikow[j][haslo] == i:
                    wyniki.append(haslo)

    najwyzszy=0
    for i in range(len(wyniki)-1):
        if wyniki[i].isnumeric() and not wyniki[i+1].isnumeric():
            najwyzszy=int(wyniki[i])
            wyniki=wyniki[i:]
            break

    for i in range(len(wyniki)):
        if wyniki[i].isnumeric() and int(wyniki[i])==najwyzszy-3:
            wyniki=wyniki[:i]
            break

    return wyniki
    


def wyszukiwanie_po_stronach(plik, metoda, ile_stron):
    slowniki=[]
    autorzy=[]
    doi=[]
    kluczowe=[]
    for i in range(ile_stron):
        tekst1=funkcja_pobierajaca_tekst(plik, metoda)[1][i]
        tekst2=funkcja_pobierajaca_tekst(plik, metoda)[0][i] # bez podziału po wielkich literach

        slowniki.append(wyszukiwanie_tytulu(tekst1))

        autorzy.append(Wyszukiwanie_autorow(tekst1))


        kluczowe+=slowa_kluczowe(tekst1)

        doi.append(DOI(tekst2))

    tytuly=slowniki_na_liste(slowniki)

    autorzy2=''
    for a in autorzy:
        autorzy2=autorzy2+' '+a

    i=0
    tytuly2=[]
    while(i<len(tytuly)):
        if not tytuly[i].isnumeric():
            tytuly2.append(tytuly[i])
        i+=1


    return [tytuly2, autorzy2, doi, kluczowe]



def na_json(plik, metoda, ile_stron):

    x=wyszukiwanie_po_stronach(plik, metoda, ile_stron)

    json_dict = {
        "title": x[0], 
        "authors": x[1].replace(',,',','),
        "doi" : x[2],
        "keywords" : x[3]
    }

    json_with_library = json.dumps(json_dict, ensure_ascii=False)  

    wynik_koncowy = []
    wynik_koncowy.append(json_with_library)
    
    return wynik_koncowy 

import sys
print(na_json(sys.argv[1], "PyPDF2", 2))

