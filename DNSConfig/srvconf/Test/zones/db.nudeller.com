; PATH=/etc/bind/zones/db.nudeller.com
; 
; DNS Zone file for nudeller.com using DNS setup Default
;
; This file is automatically generated CHANGES WILL BE OVERWRITTEN!
;
$TTL 600
nudeller.com.      IN      SOA     dns1.nudeller.com. hallam.gmail.com. (
                        2018082801       ; serial, todays date + todays serial 
                        3600              ; refresh, seconds
                        1800              ; retry, seconds
                        3600000              ; expire, seconds
                        600 )            ; minimum, seconds

; Additional name servers
nudeller.com.     IN      NS      dns1.nudeller.com.
dns1.nudeller.com.    A 178.62.79.124 
nudeller.com.     IN      NS      dns2.nudeller.com.
dns2.nudeller.com.    A 139.59.200.120 


nudeller.com.    IN     CAA 0 issue "comodoca.com"
nudeller.com.    IN     CAA 0 issue "letsencrypt.org"
nudeller.com.    IN     CAA 0 issuewild  ";"
nudeller.com.    IN     CAA 0 iodef "mailto:hostmaster@hallambaker.com"

; Hardcoded A records

; For now, forward all mail to a forwarder on the authoritative
nudeller.com.       IN      MX       1 smtp1.hallambaker.com.


; Host host1.mathmesh.com 173.76.191.93 
nudeller.com.    A 173.76.191.93 
www.nudeller.com.    A 173.76.191.93 
http.nudeller.com.    A 173.76.191.93
https.nudeller.com.    A 173.76.191.93 
_http._tcp.nudeller.com.  IN    SRV 1 1 80 host1.mathmesh.com.
_https._tcp.nudeller.com.   IN   SRV 1 1 443 host1.mathmesh.com.


