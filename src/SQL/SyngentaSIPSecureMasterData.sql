SET IDENTITY_INSERT [dbo].[ApplicationSetting] ON
GO
;merge ApplicationSetting a
using 
(
	select 1 as Id, N'CryptoKey' as [KeyName],'<RSAKeyValue><Modulus>wPssQKo+e0kNpAWa7FKDmWe/lPKOd8jQIZAKRWdLdanGRfkdChagIpOX7CPEWTK8M1uc15gGbF9u7wuFOb4NQ7mnRRDa/MWsa2xO32nS6zPr6Er8Zraz3GC93XHOpGd+R9rUMuhGtYwGYiAxDnXkMhkGZneKFzzRQ/zk4LEYraU=</Modulus><Exponent>AQAB</Exponent><P>xPQGvYcwXdrEifIGTRz0r2MAAL0sC7BX1BvcucVo35F2zyQwssWn7GwgUSzW4zxdUbO0TwXRHS5TK601RM3Lnw==</P><Q>+tZK+MoK4brPYP0okoN+IQCqB80cKC/4nVFKyutdOKQisbH8who3tlaBZCnyl+WciJWA+PntWYrxHO9EVl1AOw==</Q><DP>L+1iJoTbVDuEUD1BI0rEkFl7CrL+IOoMtWUCVYKgfqcbTlT0NMy8jF/TjW1n7WwnGVatrf0wYfV/78T6jX+26Q==</DP><DQ>WlecIQcD7FEQJ+qGFl7tqo1GsBCbt42viVmiCnvC04eqRz3Hv1EaB74YVC8XhMXaHwdZsFKzvbBhLv6uKjzjyQ==</DQ><InverseQ>tas0amcU0k2/xj48jhI/0eP/aLBo3jP/4zyc1ys7MNyPXyBN8Q6ryN8xvrQQGGv4Oy0sgFUXpm+xPVdGew5Llg==</InverseQ><D>F15S/YGgD03JWRNUr0lOpkTp7h9xIRAwI+BNXhokCRuF5vNDkPUsbwNwYifyntqF77IoS/Mc1AK6bDf46gUZleOr3cwY4lZabb482CPj+LAUz3U8XHV7KHbSTzVnaIQxSOuc1wwiZ8TXb3WA+Nlkq2rtfgdDuXxDa7NpERKWtAE=</D></RSAKeyValue>' as [Value]
)s on a.Id = s.Id
when not matched then 
	insert (Id,KeyName,Value) 
	values(s.Id,s.KeyName,s.Value)
;
SET IDENTITY_INSERT [dbo].[ApplicationSetting] OFF
GO