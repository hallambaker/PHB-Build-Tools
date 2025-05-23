; PATH=/etc/bind/zones/db.thresholdshare.com
; 
; DNS Zone file for thresholdshare.com using DNS setup Default
;
; This file is automatically generated CHANGES WILL BE OVERWRITTEN!
;
$TTL 600
thresholdshare.com.      IN      SOA     dns1.thresholdshare.com. hallam.gmail.com. (
                        2025013001       ; serial, todays date + todays serial 
                        3600              ; refresh, seconds
                        1800              ; retry, seconds
                        3600000              ; expire, seconds
                        600 )            ; minimum, seconds

; Additional name servers
thresholdshare.com.     IN      NS      dns1.thresholdshare.com.
dns1.thresholdshare.com.    A 178.62.79.124 
thresholdshare.com.     IN      NS      dns2.thresholdshare.com.
dns2.thresholdshare.com.    A 139.59.200.120 


thresholdshare.com.    IN     CAA 0 issue "comodoca.com"
thresholdshare.com.    IN     CAA 0 issue "letsencrypt.org"
thresholdshare.com.    IN     CAA 0 issuewild  ";"
thresholdshare.com.    IN     CAA 0 iodef "mailto:hostmaster@hallambaker.com"

; Hardcoded A records

; default mail
thresholdshare.com.       IN      MX       10 mx01.ionos.com.
thresholdshare.com.       IN      MX       10 mx00.ionos.com.
thresholdshare.com. TXT "v=spf1 redirect=_spf.google.com"



; Host host1.mathmesh.com 178.62.79.124 
thresholdshare.com.    A 178.62.79.124 
www.thresholdshare.com.    A 178.62.79.124 
http.thresholdshare.com.    A 178.62.79.124
https.thresholdshare.com.    A 178.62.79.124 
_http._tcp.thresholdshare.com.  IN    SRV 1 1 80 host1.mathmesh.com.
_https._tcp.thresholdshare.com.   IN   SRV 1 1 443 host1.mathmesh.com.


; handles
