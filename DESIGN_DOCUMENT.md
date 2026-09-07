# PowerTycoon - Design Document

## 1. Spielkonzept

**PowerTycoon** ist ein Management-Simulation über Energieproduktion. Der Spieler übernimmt die Rolle eines Energie-Tycoons und muss sein Kraftwerk-Imperium aufbauen und verwalten.

## 2. Kern-Spielmechaniken

### 2.1 Kraftwerk-Typen

| Typ | Kosten | Output | Wartung | Besonderheit |
|-----|--------|--------|---------|--------------|
| **Kohle** | 100k$ | Hoch | Hoch | Hohe Umweltbelastung |
| **Gas** | 80k$ | Mittel | Mittel | Relativ sauber |
| **Wind** | 120k$ | Variabel | Niedrig | Wetterabhängig |
| **Solar** | 100k$ | Variabel | Niedrig | Tagesabhängig |
| **Kern** | 500k$ | Sehr Hoch | Sehr Hoch | Teuer, aber mächtig |

### 2.2 Ressourcen-Management

- **Energie**: Erzeugt, gelagert, verkauft
- **Rohstoffe**: Kohle, Gas, Uran (müssen gekauft werden)
- **Wasser**: Für Kühlung notwendig
- **Geld**: Budget für Bau und Betrieb
- **Reputation**: Beeinflusst Preise und Genehmigungen

### 2.3 Wirtschafts-Simulator

- Energiepreise schwanken basierend auf Angebot/Nachfrage
- Verträge mit Stadtteilen und Industrien
- Steuern und Regulierungen
- Klimaziele und grüne Energie-Bonuses

## 3. UI/UX Design

### Haupt-Menü
- Neues Spiel
- Laden
- Einstellungen
- Über

### Gameplay-HUD
- Energieproduktion (Live-Anzeige)
- Finanz-Dashboard
- Kraftwerk-Verwaltung
- Nachrichtenbereich
- Ereignis-Log

### Gebäude-Menü
- Verfügbare Kraftwerk-Typen
- Kosten und Auswirkungen
- Platzierung auf der Map

## 4. Grafik & Atmosphäre (HDRP)

- Industrielle Ästhetik
- Lichter: Neon-UI, nächtliche Kraftwerks-Lichter
- Partikel-Effekte: Dampf, Abgase, Regenbogen-Effekte
- Kamera: Top-Down / Isometrisch mit Zoom
- Tageszeit-Wechsel

## 5. Audio-Konzept

- Ambient-Sounds: Maschinenlärm, Windturbinen
- UI-Feedback-Sounds
- Musik: Ruhig, ambient, technisch
- Event-Sounds: Alarme bei Problemen

## 6. Progression

### Early Game (0-30 Min)
- Tutorial: Erstes Kraftwerk bauen
- Grundlegende Mechaniken erlernen

### Mid Game (30-120 Min)
- Mehrere Kraftwerke
- Netzwerk-Management
- Komplexere Entscheidungen

### Late Game (120+ Min)
- Große Expansion
- Ernsthafte Finanzentscheidungen
- Übernahme von Konkurrenten

## 7. Technisches Setup (Unity 6 HDRP)

### Anforderungen
- HDRP 18+ (oder aktuelle Version für Unity 6)
- C# 11+
- Mindestens 4GB RAM empfohlen

### Architektur
- Scene-Management: Modular
- Object Pooling für häufig instanziierte Objekte
- Job System für Performance
- Asset Pipeline optimiert für HDRP

### Szenen-Struktur
1. `MainMenu` - Hauptmenü
2. `Game` - Haupt-Spielszene
3. `Settings` - Einstellungsmenü
4. `LoadGame` - Lade-Bildschirm

## 8. Entwicklungs-Roadmap

### Phase 1: Core Systems
- [ ] Spielwelt und Szenen
- [ ] Kraftwerk-System
- [ ] Energie-Produktion und -Verkauf
- [ ] Basis-UI

### Phase 2: Management
- [ ] Finanz-System
- [ ] Ressourcen-Management
- [ ] Speicher/Laden-System
- [ ] Event-System

### Phase 3: Polish
- [ ] Grafik-Optimierung
- [ ] Audio-Integration
- [ ] UI-Überarbeitung
- [ ] Balance-Tweaks

### Phase 4: Features
- [ ] Kampagnen/Szenarios
- [ ] Achievements
- [ ] Statistiken/Leaderboards

## 9. Zielgruppe

- Fans von Management-Spielen (Two Point Hospital, Tycoon-Spiele)
- Altersgruppe: 12+
- Spielmodus: Single-Player (später evtl. Multiplayer)

## 10. Erfolgs-Kriterien

- ✅ Stabiles Gameplay-Loop
- ✅ Visuell beeindruckend mit HDRP
- ✅ Gutes Balancing
- ✅ Intuitive Bedienung
