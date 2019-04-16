<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MusicScoreGen: music score generator</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <H1>(Simple) Music Score Generator</H1>
    <p style="text-align: center"><asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label></p>
    <table>
      <tr>
        <td>Output format: </td>
        <td><asp:DropDownList ID="ddlFormato" runat="server">
          <asp:ListItem>PDF</asp:ListItem>
          <asp:ListItem>Lilypond</asp:ListItem>
          </asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td>Time signature: </td>
        <td><asp:DropDownList ID="ddlCompas" runat="server">
          <asp:ListItem>2/4</asp:ListItem>
          <asp:ListItem>3/4</asp:ListItem>
          <asp:ListItem Selected="True">4/4</asp:ListItem>
          <asp:ListItem>2/2</asp:ListItem>
          <asp:ListItem>6/8</asp:ListItem>
          <asp:ListItem>9/8</asp:ListItem>
          <asp:ListItem>12/8</asp:ListItem>
          </asp:DropDownList>
        </td>
      </tr>
      <tr>
       <td>Number of measures: </td>
       <td><asp:TextBox ID="tbCompases" MaxLength="2" runat="server"></asp:TextBox>
           <asp:RequiredFieldValidator ID="rfvCompases" runat="server" ControlToValidate="tbCompases" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
           <asp:RangeValidator ID="rvCompases" runat="server" ControlToValidate="tbCompases" Display="Dynamic" Type="Integer" MinimumValue="1" MaximumValue="99" ErrorMessage="It must be an integer between 1 and 99"></asp:RangeValidator>
       </td>
      </tr>
      <tr>
        <td>Key signature: </td>
        <td>
            <asp:RadioButtonList ID="rblTonalidad" runat="server" RepeatColumns="6" RepeatDirection="Horizontal">
                <asp:ListItem Value="C" Selected="True"><img src="images/C.png" /></asp:ListItem>
                <asp:ListItem Value="G"><img src="images/G.png" /></asp:ListItem>
                <asp:ListItem Value="F"><img src="images/F.png" /></asp:ListItem>
                <asp:ListItem Value="D"><img src="images/D.png" /></asp:ListItem>
                <asp:ListItem Value="Bb"><img src="images/Bf.png" /></asp:ListItem>
                <asp:ListItem Value="A"><img src="images/A.png" /></asp:ListItem>
                <asp:ListItem Value="Eb"><img src="images/Ef.png" /></asp:ListItem>
                <asp:ListItem Value="E"><img src="images/E.png" /></asp:ListItem>
                <asp:ListItem Value="Ab"><img src="images/Af.png" /></asp:ListItem>
                <asp:ListItem Value="B"><img src="images/B.png" /></asp:ListItem>
                <asp:ListItem Value="Db"><img src="images/Df.png" /></asp:ListItem>
                <asp:ListItem Value="F#"><img src="images/Fs.png" /></asp:ListItem>
                <asp:ListItem Value="Gb"><img src="images/Gf.png" /></asp:ListItem>
                <asp:ListItem Value="C#"><img src="images/Cs.png" /></asp:ListItem>
                <asp:ListItem Value="Cb"><img src="images/Cf.png" /></asp:ListItem>
            </asp:RadioButtonList>
            </td>
      </tr>
    </table>
    <fieldset>
      <legend><img src="images/treble.png" /></legend>
      <table>
        <tr>
          <td>Note range: </td>
          <td>
            <asp:DropDownList ID="ddlRangoG1A" runat="server">
              <asp:ListItem Selected="True">C</asp:ListItem>
              <asp:ListItem>D</asp:ListItem>
              <asp:ListItem>E</asp:ListItem>
              <asp:ListItem>F</asp:ListItem>
              <asp:ListItem>G</asp:ListItem>
              <asp:ListItem>A</asp:ListItem>
              <asp:ListItem>B</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="ddlRangoG1B" runat="server">
              <asp:ListItem>3</asp:ListItem>
              <asp:ListItem Selected="True">4</asp:ListItem>
              <asp:ListItem>5</asp:ListItem>
              <asp:ListItem>6</asp:ListItem>
            </asp:DropDownList>
            -
            <asp:DropDownList ID="ddlRangoG2A" runat="server">
              <asp:ListItem>C</asp:ListItem>
              <asp:ListItem>D</asp:ListItem>
              <asp:ListItem>E</asp:ListItem>
              <asp:ListItem>F</asp:ListItem>
              <asp:ListItem Selected="True">G</asp:ListItem>
              <asp:ListItem>A</asp:ListItem>
              <asp:ListItem>B</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="ddlRangoG2B" runat="server">
              <asp:ListItem>3</asp:ListItem>
              <asp:ListItem Selected="True">4</asp:ListItem>
              <asp:ListItem>5</asp:ListItem>
              <asp:ListItem>6</asp:ListItem>
            </asp:DropDownList>
          </td>
        </tr>
        <tr>
          <td>Rythm blocks: </td>
          <td>
              <asp:CheckBoxList ID="cblBloquesRitmicosG" runat="server" RepeatColumns="6" RepeatDirection="Horizontal">
                  <asp:ListItem Value="1"><img src="images/br_1.png" /></asp:ListItem>
                  <asp:ListItem Value="2"><img src="images/br_2.png" /></asp:ListItem>
                  <asp:ListItem Value="4"><img src="images/br_4.png" /></asp:ListItem>
                  <asp:ListItem Value="8"><img src="images/br_8.png" /></asp:ListItem>
                  <asp:ListItem Value="2."><img src="images/br_2dot.png" /></asp:ListItem>
                  <asp:ListItem Value="4."><img src="images/br_4dot.png" /></asp:ListItem>
                  <asp:ListItem Value="8+8"><img src="images/br_8_8.png" /></asp:ListItem>
                  <asp:ListItem Value="16+16+16+16"><img src="images/br_16_16_16_16.png" /></asp:ListItem>
                  <asp:ListItem Value="8+4+8"><img src="images/br_8_4_8.png" /></asp:ListItem>
                  <asp:ListItem Value="8+4"><img src="images/br_8_4.png" /></asp:ListItem>
                  <asp:ListItem Value="4+8"><img src="images/br_4_8.png" /></asp:ListItem>
                  <asp:ListItem Value="16.+8"><img src="images/br_16dot_8.png" /></asp:ListItem>
                  <asp:ListItem Value="8+16."><img src="images/br_8_16dot.png" /></asp:ListItem>
              </asp:CheckBoxList>
          </td>
        </tr>
      </table>
    </fieldset>
    <fieldset>
      <legend><img src="images/bass.png" /></legend>
      <table>
        <tr>
          <td>Note range: </td>
          <td>
            <asp:DropDownList ID="ddlRangoF1A" runat="server">
              <asp:ListItem Selected="True">C</asp:ListItem>
              <asp:ListItem>D</asp:ListItem>
              <asp:ListItem>E</asp:ListItem>
              <asp:ListItem>F</asp:ListItem>
              <asp:ListItem>G</asp:ListItem>
              <asp:ListItem>A</asp:ListItem>
              <asp:ListItem>B</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="ddlRangoF1B" runat="server">
              <asp:ListItem>1</asp:ListItem>
              <asp:ListItem>2</asp:ListItem>
              <asp:ListItem Selected="True">3</asp:ListItem>
              <asp:ListItem>4</asp:ListItem>
            </asp:DropDownList>
            -
            <asp:DropDownList ID="ddlRangoF2A" runat="server">
              <asp:ListItem>C</asp:ListItem>
              <asp:ListItem>D</asp:ListItem>
              <asp:ListItem>E</asp:ListItem>
              <asp:ListItem>F</asp:ListItem>
              <asp:ListItem Selected="True">G</asp:ListItem>
              <asp:ListItem>A</asp:ListItem>
              <asp:ListItem>B</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="ddlRangoF2B" runat="server">
              <asp:ListItem>1</asp:ListItem>
              <asp:ListItem>2</asp:ListItem>
              <asp:ListItem Selected="True">3</asp:ListItem>
              <asp:ListItem>4</asp:ListItem>
            </asp:DropDownList>
          </td>
        </tr>
        <tr>
          <td>Rythm blocks: </td>
          <td>
              <asp:CheckBoxList ID="cblBloquesRitmicosF" runat="server" RepeatColumns="6" RepeatDirection="Horizontal">
                  <asp:ListItem Value="1"><img src="images/br_1.png" /></asp:ListItem>
                  <asp:ListItem Value="2"><img src="images/br_2.png" /></asp:ListItem>
                  <asp:ListItem Value="4"><img src="images/br_4.png" /></asp:ListItem>
                  <asp:ListItem Value="8"><img src="images/br_8.png" /></asp:ListItem>
                  <asp:ListItem Value="2."><img src="images/br_2dot.png" /></asp:ListItem>
                  <asp:ListItem Value="4."><img src="images/br_4dot.png" /></asp:ListItem>
                  <asp:ListItem Value="8+8"><img src="images/br_8_8.png" /></asp:ListItem>
                  <asp:ListItem Value="16+16+16+16"><img src="images/br_16_16_16_16.png" /></asp:ListItem>
                  <asp:ListItem Value="8+4+8"><img src="images/br_8_4_8.png" /></asp:ListItem>
                  <asp:ListItem Value="8+4"><img src="images/br_8_4.png" /></asp:ListItem>
                  <asp:ListItem Value="4+8"><img src="images/br_4_8.png" /></asp:ListItem>
                  <asp:ListItem Value="16.+8"><img src="images/br_16dot_8.png" /></asp:ListItem>
                  <asp:ListItem Value="8+16."><img src="images/br_8_16dot.png" /></asp:ListItem>
              </asp:CheckBoxList>
          </td>
        </tr>
      </table>
    </fieldset>
    <p style="text-align: center"><asp:Button ID="btnEnviar" runat="server" Text="Submit" onclick="btnEnviar_Click" /></p>
    </div>
    </form>
</body>
</html>
