; PATH=/etc/bind/zones/db.mplace2.com
; 
; DNS Zone file for mplace2.com using DNS setup Default
;
; This file is automatically generated CHANGES WILL BE OVERWRITTEN!
;
$TTL 600
mplace2.com.      IN      SOA     dns1.mplace2.com. hallam.gmail.com. (
                        2024121001       ; serial, todays date + todays serial 
                        3600              ; refresh, seconds
                        1800              ; retry, seconds
                        3600000              ; expire, seconds
                        600 )            ; minimum, seconds

; Additional name servers
mplace2.com.     IN      NS      dns1.mplace2.com.
dns1.mplace2.com.    A 178.62.79.124 
mplace2.com.     IN      NS      dns2.mplace2.com.
dns2.mplace2.com.    A 139.59.200.120 


mplace2.com.    IN     CAA 0 issue "comodoca.com"
mplace2.com.    IN     CAA 0 issue "letsencrypt.org"
mplace2.com.    IN     CAA 0 issuewild  ";"
mplace2.com.    IN     CAA 0 iodef "mailto:hostmaster@hallambaker.com"

; Hardcoded A records

; For now, forward all mail to a forwarder on the authoritative
mplace2.com.       IN      MX       1 smtp1.hallambaker.com.


; Host host1.mathmesh.com 178.62.79.124 
mplace2.com.    A 178.62.79.124 
www.mplace2.com.    A 178.62.79.124 
http.mplace2.com.    A 178.62.79.124
https.mplace2.com.    A 178.62.79.124 
_http._tcp.mplace2.com.  IN    SRV 1 1 80 host1.mathmesh.com.
_https._tcp.mplace2.com.   IN   SRV 1 1 443 host1.mathmesh.com.


