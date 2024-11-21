# bakircay-2024-gd-220601053

# Bakırçay Fall-2024 Game Development

## Proje Özeti

Bu proje, Unity kullanarak basitleştirilmiş bir 3D eşleştirme oyununun geliştirilmesini içeren bir görevdir.

Projenin amacı, kullanıcıların rastgele dağılmış 3D objeleri bir yerleştirme alanına yerleştirerek eşleştirme yapmalarını sağlamaktır. Oyun, temel nesne etkileşimi, birden fazla obje, yerleştirme alanı ve animasyon içermektedir.

## Oyun Özeti

Bu oyun, oyuncuların ekrandaki objeleri alıp yerleştirme alanına yerleştirmeleri gereken basit bir 3D eşleştirme oyunudur. Obje yerleştirme işlemi, animasyonlu platforma düşen objelerle desteklenmiştir. Ayrıca UI üzerinden oyuncunun skoru da gösterilmektedir.

### Oyun Kuralları:
1. Oyuncu, ekrandaki rastgele dağılmış 3D objeleri alıp, yerleştirme alanına yerleştirebilir.
2. Oyuncu farklı obje koyması durumunda objeler etrafa kuvvet sayesinde fırlamaktadır.
3. Eşleşen objeler yok edilir ve bu işlem animasyonla görselleştirilir.
4. Oyuncunun skoru, yerleştirilen doğru nesnelerle artar.

## Gameplay Önizlemesi

Aşağıda oyunun işleyişini gösteren kısa bir ekran kaydı bulunmaktadır. Kaydı izleyerek oyun hakkında daha fazla bilgi edinebilirsiniz.

[Oyun Video Önizlemesi](link-video)

## Proje Yapısı

Bu proje şu klasörlerden oluşmaktadır:
- **Animations**: Animasyon dosyalarının bulunduğu klasör.
- **Materials**: Oyanan zemin gibi objelerin materyalleri.
- **Models**: Objelerin modelleri.
- **Prefabs**: Her türlü objenin olduğu prefab klasörü.
- **Scenes**: Oyun sahneleri.
- **Scripts**: Oyun mantığını kontrol eden C# script dosyaları.
  

## Özellikler

- **Temel Oyun Alanı**: Oyun alanı, 3D objelerle rastgele dağılmıştır.
- **Yerleştirme Alanı**: Objelerin yerleştirilebileceği bir alan.
- **Objelerin Taşınması**: Oyuncu, objeleri sürükleyip yerleştirme alanına bırakabilir.
- **Animasyonlar**: Objeler yerleştirilip eşleştiğinde platform animasyonu ile yok edilir.
- **Skor Takibi**: UI üzerinden oyuncunun skoru takip edilir.

## Bonus Özellikler

Bu aşamada bonus olarak, oyun alanındaki objelerin düşme platformuna yerleştirilmesi animasyonlarla desteklenmiştir. Ayrıca, oyuncunun ilerleyişini takip etmek amacıyla skor UI'si eklenmiştir.

## Kullanılan Teknolojiler

- **Unity 2022.3.50**: Oyun geliştirme platformu.
- **C#**: Oyun mantığının yazıldığı programlama dili.
- **Animasyonlar**: Objelerin yerleştirilmesi ve eşleşmesi sırasında kullanılan animasyonlar.

